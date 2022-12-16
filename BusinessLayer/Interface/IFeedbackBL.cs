using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IFeedbackBL
    {
        public FeedbackModel AddFeedback(int userId, int bookId, FeedbackModel model);
        public List<FeedbackModel> MyFeedbacks(int userId);
        public bool DeleteFeedback(int feedbackId, int userId);
    }
}
