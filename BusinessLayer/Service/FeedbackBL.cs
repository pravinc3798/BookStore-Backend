using BusinessLayer.Interfaces;
using CommonLayer.Model;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class FeedbackBL : IFeedbackBL
    {
        private readonly IFeedbackRL feedbackRL;

        public FeedbackBL(IFeedbackRL feedbackRL)
        {
            this.feedbackRL = feedbackRL;
        }

        public FeedbackModel AddFeedback(int userId, int bookId, FeedbackModel model)
        {
            try
            {
                return feedbackRL.AddFeedback(userId, bookId, model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<FeedbackModel> MyFeedbacks(int userId)
        {
            try
            {
                return feedbackRL.MyFeedbacks(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteFeedback(int feedbackId, int userId)
        {
            try
            {
                return feedbackRL.DeleteFeedback(feedbackId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
