using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<UserDetails> userDetails { get; set; }
        DbSet<OtpHistory> otpHistory { get; set; }
        DbSet<BubbleDetails> bubbleDetails { get; set; }
        DbSet<BubbleMeetDetails> bubbleMeetDetails { get; set; }
        DbSet<PodDetails> podDetails { get; set; }
        DbSet<BubbleMembers> bubbleMembers { get; set; }
        DbSet<Counties> counties { get; set; }
        DbSet<PODMembers> podMembers { get; set; }
        DbSet<PODBubbleMembers> podBubbleMembers { get; set; }
        DbSet<CheckListQuestionType> checkListQuestionTypes { get; set; }
        DbSet<CheckListSubjectiveAnswerQuestion> checkListSubjectiveAnswerQuestion { get; set; }
        DbSet<CheckListSubjectiveQuestion_Answers> checkListSubjectiveQuestion_Answers { get; set; }
        DbSet<CheckListQuestionOption> checkListQuestionOption { get; set; }
        DbSet<BubbleMeetMembers> bubbleMeetMembers { get; set; }
        DbSet<BubbleMeetMemberPermissions> bubbleMeetMemberPermissions { get; set; }
        DbSet<CheckListDetails> checkListDetails { get; set; }
        DbSet<PODMeetDetails> podMeetDetails { get; set; }
        DbSet<PODMeetMembers> podMeetMembers { get; set; }
        DbSet<UserDeviceDetails> userDeviceDetails { get; set; }
        DbSet<Notifications> notifications { get; set; }
        DbSet<NotificationsHistory> notificationsHistory { get; set; }
        DbSet<ChatDetails> chatDetails { get; set; }
        DbSet<ChatMembers> chatMembers { get; set; }
        DbSet<ChatHistory> chatHistory { get; set; }
        DbSet<BubbleSafetyDetails> bubbleSafetyDetails { get; set; }
        DbSet<BubbleSafetyWights> bubbleSafetyWights { get; set; }
        DbSet<BubbleSafetyCalculationFields> bubbleSafetyWightsCalculationFields { get; set; }
        Task<int> SaveChanges();
    }
}