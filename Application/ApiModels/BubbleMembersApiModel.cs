using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ApiModels
{
   public class BubbleMembersApiModel:BaseApiModel
    {
        public BubbleMembersApiModel() {
            BubblesList = new List<BubbleApiModel>();
            UserList = new List<UserApiModels>();
            BubbleMeetDetailsApiModelList = new List<BubbleMeetDetailsApiModel>();
        }
        public int BubbleId { get; set; }
        public int UserId { get; set; }
        public bool IsAdmin { get; set; }
        public BubbleApiModel Bubble { get; set; }
        public UserApiModels User { get; set; }
        public List<BubbleApiModel> BubblesList { get; set; }
        public List<UserApiModels> UserList { get; set; }
        public List<BubbleMeetDetailsApiModel> BubbleMeetDetailsApiModelList { get; set; }
    }
}
