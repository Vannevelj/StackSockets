namespace Library.Requests
{
    public enum Activity
    {
        CommentAdd,
        PostEdit,
        ScoreChange,
        AnswerAdd,
        ActiveQuestions,
        NewestQuestionsByTag,
        AnswerAccept,
        AnswerUnaccept,
        DashboardActivity
    }

    public enum ReviewQueue
    {
        SuggestedEdits = 1,
        CloseVotes = 2,
        LowQualityPosts = 3,
        FirstPosts = 4,
        LateAnswers = 5
    }
}