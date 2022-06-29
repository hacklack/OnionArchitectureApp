using Application.ApiModels;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Domain.CommonCodes.CommonEnums;

namespace Application.ChatComponents.Hubs
{
    public class ChatHubs : Hub
    {
        private readonly IApplicationDbContext _context;
        public ChatHubs(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SendMessage(int chatId, int userId, string chatMessage)
        {
            string chatParentName=string.Empty;
            string notificationDescription = string.Empty;
            NotificationFCMApiModel notification = _context.notifications
                  .Where(y => y.NotificationTypeId == NotificationTypes.MessageReceived)
                  .Select(x => new NotificationFCMApiModel
                  {
                      Title = x.Title,
                      Description = x.Description,
                      NotificationTypeId = x.NotificationTypeId,
                      Id = x.Id
                  }).FirstOrDefault();

            var chatDetails = _context.chatDetails.Where(y => y.Id == chatId).FirstOrDefault();
            if (chatDetails.ChatParentTypeId == (int)MeetType.Bubble)
            {
                chatParentName = _context.bubbleDetails.Where(y => y.Id == chatDetails.ChatParentId).Select(x => x.BubbleName).FirstOrDefault();
            }
            else if (chatDetails.ChatParentTypeId == (int)MeetType.BubbleMeet)
            {
                chatParentName = _context.bubbleMeetDetails.Where(y => y.Id == chatDetails.ChatParentId).Select(x => x.Title).FirstOrDefault();
            }
            else if (chatDetails.ChatParentTypeId == (int)MeetType.POD)
            {
                chatParentName = _context.podDetails.Where(y => y.Id == chatDetails.ChatParentId).Select(x => x.PODName).FirstOrDefault();
            }
            else if (chatDetails.ChatParentTypeId == (int)MeetType.PODMeet)
            {
                chatParentName = _context.podMeetDetails.Where(y => y.Id == chatDetails.ChatParentId).Select(x => x.Title).FirstOrDefault();
            }


            UserApiModels user = _context.userDetails
                                .Where(y => y.Id == userId)
                                .Select(x => new UserApiModels
                                {
                                    Id = x.Id,
                                    Username = x.Username,
                                    PhoneNo = x.PhoneNo,
                                    ProfilePicUrl = x.ProfilePicUrl
                                }).FirstOrDefault();


            List<int> lstChatMembers = _context.chatMembers
                                   .Where(y => y.ChatId == chatId && y.ChatMemberId != userId && y.ChatMemberStatus == true)
                                   .Select(x => x.ChatMemberId).ToList();

            List<string> chatUsersIds = new List<string>();
            foreach (var item in lstChatMembers)
            {
                NotificationsServices notificationsServices = new NotificationsServices(_context);
                notificationDescription = notification.Title + " from " + user.Username +" in "+ chatParentName ;
                await notificationsServices.SendNotification(notification.Title, notificationDescription, notification.Id, chatId, item, userId, userId, NotificationTypeChild.ChatMessageNotification, NotificationCategories.General);
                chatUsersIds.Add(Convert.ToString(item));
            }

            ChatHistory newMessage = new ChatHistory() { ChatId = chatId, ChatMessageSenderId = userId, ChatMessage = chatMessage, CreatedBy = userId, UpdatedBy = userId,CreatedOn=DateTime.UtcNow,UpdatedOn=DateTime.UtcNow };

            if (newMessage != null)
            {
                _context.chatHistory.Add(newMessage);
                await _context.SaveChanges();
            }

            string data = JsonConvert.SerializeObject(new { newMessage = newMessage, senderName = user.Username, messageSent = DateTime.UtcNow.ToLongDateString() });
            await Clients.All.SendAsync("ReceiveMessage", chatId, userId, data);
            // await Clients.Users(chatUsersIds).SendAsync("ReceiveMessage", chatId, userId, data);

        }

        //public async Task SendToRoom(string roomName, string message)
        //{
        //    try
        //    {
        //        var user = _context.Users.Where(u => u.UserName == IdentityName).FirstOrDefault();
        //        var room = _context.Rooms.Where(r => r.Name == roomName).FirstOrDefault();

        //        if (!string.IsNullOrEmpty(message.Trim()))
        //        {
        //            // Create and save message in database
        //            var msg = new Message()
        //            {
        //                Content = Regex.Replace(message, @"(?i)<(?!img|a|/a|/img).*?>", string.Empty),
        //                FromUser = user,
        //                ToRoom = room,
        //                Timestamp = DateTime.Now
        //            };
        //            _context.Messages.Add(msg);
        //            _context.SaveChanges();

        //            // Broadcast the message
        //            var messageViewModel = _mapper.Map<Message, MessageViewModel>(msg);
        //            await Clients.Group(roomName).SendAsync("newMessage", messageViewModel);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        await Clients.Caller.SendAsync("onError", "Message not send! Message should be 1-500 characters.");
        //    }
        //}
    }
}
