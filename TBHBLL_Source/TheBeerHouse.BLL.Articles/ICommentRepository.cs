namespace TheBeerHouse.BLL.Articles
{
    using System;
    using System.Collections.Generic;

    public interface ICommentRepository
    {
        /// <summary>
        /// </summary>
        /// <param name="vComment"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        bool AddComment(Comment vComment);
        /// <summary>
        /// </summary>
        /// <param name="CommentId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        bool ApproveComment(int CommentId);
        bool DeleteComment(Comment vComment);
        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        List<Comment> GetApprovedComments();
        /// <summary>
        /// </summary>
        /// <param name="CommentId"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        Comment GetCommentById(int CommentId);
        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        int GetCommentCount();
        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        List<Comment> GetComments();
        bool UnDeleteComment(Comment vComment);
        /// <summary>
        /// </summary>
        /// <param name="vComment"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        bool UpdateComment(Comment vComment);
    }
}

