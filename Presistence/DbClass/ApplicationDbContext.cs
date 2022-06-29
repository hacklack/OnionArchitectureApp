using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.DbClass
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<UserDetails> userDetails { get; set; }
        public DbSet<OtpHistory> otpHistory { get; set; }
        public DbSet<BubbleDetails> bubbleDetails { get; set; }
        public DbSet<BubbleMeetDetails> bubbleMeetDetails { get; set; }
        public DbSet<PodDetails> podDetails { get; set; }
        public DbSet<BubbleMembers> bubbleMembers { get; set; }
        public DbSet<Counties> counties { get; set; }
        public DbSet<PODBubbleMembers> podBubbleMembers { get; set; }
        public DbSet<PODMembers> podMembers { get; set; }
        public DbSet<CheckListQuestionType> checkListQuestionTypes { get; set; }
        public DbSet<CheckListSubjectiveAnswerQuestion> checkListSubjectiveAnswerQuestion { get; set; }
        public DbSet<CheckListSubjectiveQuestion_Answers> checkListSubjectiveQuestion_Answers { get; set; }
        public DbSet<CheckListQuestionOption> checkListQuestionOption { get; set; }

        public DbSet<BubbleMeetMembers> bubbleMeetMembers { get; set; }
        public DbSet<BubbleMeetMemberPermissions> bubbleMeetMemberPermissions { get; set; }
        public DbSet<CheckListDetails> checkListDetails { get; set; }
        public DbSet<PODMeetDetails> podMeetDetails { get; set; }
        public DbSet<PODMeetMembers> podMeetMembers { get; set; }
        public DbSet<UserDeviceDetails> userDeviceDetails { get; set; }
        public DbSet<Notifications> notifications { get; set; }
        public DbSet<NotificationsHistory> notificationsHistory { get; set; }
        public DbSet<ChatDetails> chatDetails { get; set; }
        public DbSet<ChatMembers> chatMembers { get; set; }
        public DbSet<ChatHistory> chatHistory { get; set; }
        public DbSet<BubbleSafetyDetails> bubbleSafetyDetails { get; set; }
        public DbSet<BubbleSafetyWights> bubbleSafetyWights { get; set; }
        public DbSet<BubbleSafetyCalculationFields> bubbleSafetyWightsCalculationFields { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }
    }
}
