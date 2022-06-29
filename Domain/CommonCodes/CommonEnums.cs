using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.CommonCodes
{
    public class CommonEnums
    {
        public enum BubbleType
        {
            Single = 1,
            Multi = 2
        }

        public enum UserPermission
        {
            IsAdmin = 0,
            IsChatAllowed = 1,
            CanEdit = 2
        }
        public enum CheckListType
        {
            BubbleCheckList = 1,
            PODCheckList = 2,
            BubbleMeetChecklist = 3,
            PODMeetChecklist = 4
        }
        public enum ServiceErrorCodes
        {
            InvalidPhoto = 1
        }
        public enum CheckListQuestionTypes
        {
            [Display(Name = "Multiple Choice Questions")]
            MultipleChoiceQuestions = 1,

            [Display(Name = "Single Choice Questions")]
            SingleChoiceQuestions = 2,

            [Display(Name = "Subjective")]
            Subjective = 3,

            [Display(Name = "Yes No Answer")]
            YesNoAnswer = 4,

            [Display(Name = "Dropdown")]
            Dropdown = 5,

            [Display(Name = "Slider")]
            Slider = 6
        }
        public enum MeetType
        {
            Bubble = 1,
            POD = 2,
            BubbleMeet = 3,
            PODMeet = 4,
        }
        public enum DeviceType
        {
            Ios = 1,
            Andriod = 2
        }
        public enum NotificationTypes
        {
            AddedTo = 1,
            MessageReceived = 2,
            BubbleCreated = 3,
            PODCreated = 4,
            BubbleMeetCreated = 5,
            PODMeetCreated = 6
        }
        public enum NotificationTypeChild
        {
            BubbleNotification = 1,
            PODNotification = 2,
            BubbleMeeetNotification = 3,
            PODMeetNotification = 4,
            ChatMessageNotification = 5
        }
        public enum NotificationCategories
        {
            General = 1,
            Reminder = 2

        }
        public enum ChatTypes
        {
            PersonalChat = 1,
            GroupChat = 2
        }
        public enum BubbleSaftyFieldType
        {
            TestPositivityRatio = 1,
            CaseDensityRatio=2,
            InfectionRatio=3,
            InfectionRatioC190=4,
            ActualCases = 5,
            ActualDeaths = 6,
            ActualVaccineCompleted=7,
            VaccineToPopulationRatio=8,
            DeathToPopulationRatio=9,
            CasesToPopulationRatio=10

        }
        public enum BubbleSaftyType
        {
            BubbleSaftyLevel = 1,
            PODSaftyLevel = 2
        }
    }
}
