using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ApiModels
{
    public class PodBubbleMembersApiModel : BaseApiModel
    {
        public PodBubbleMembersApiModel() {
            BubblesList = new List<BubbleApiModel>();
            UserList = new List<UserApiModels>();
            BubbleMeetDetailsApiModelList = new List<BubbleMeetDetailsApiModel>();
            PODList = new List<PodDetailsApiModel>();
            Pod = new PodDetailsApiModel();
        }
        public int BubbleId { get; set; }
        public int UserId { get; set; }
        public BubbleApiModel Bubble { get; set; }
        public UserApiModels User { get; set; }
        public PodDetailsApiModel Pod{ get; set; }
        public List<BubbleApiModel> BubblesList { get; set; }
        public List<PodDetailsApiModel> PODList { get; set; }
        public List<UserApiModels> UserList { get; set; }
        public List<BubbleMeetDetailsApiModel> BubbleMeetDetailsApiModelList { get; set; }
    }
}
