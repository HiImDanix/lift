namespace GuessingGame.DTO.responses
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string QuestionText { get; set; }
        public string Category { get; set; }
        public byte[] RowVer { get; set; }
        public List<AnswerDTO> Answers { get; set; }
    }
}
