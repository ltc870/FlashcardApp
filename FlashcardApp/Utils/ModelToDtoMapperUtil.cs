using FlashcardApp.DTOs.FlashcardDTO;
using FlashcardApp.DTOs.StudySessionDto;
using FlashcardApp.Models.Flashcard;
using FlashcardApp.Models.StudySession;

namespace FlashcardApp.Utils;

public static class ModelToDtoMapperUtil
{
    public static FlashcardDto MapFlashCardToDto(Flashcard flashcardModel)
    {
        return new FlashcardDto
        (
            flashcardModel.FlashcardId,
            flashcardModel.FrontText,
            flashcardModel.BackText,
            flashcardModel.StackName
        );
    }
    
    public static StudySessionDto MapStudySessionToDto(StudySession studySessionModel)
    {
        return new StudySessionDto
        (
            studySessionModel.StudySessionId,
            studySessionModel.Date,
            studySessionModel.Score,
            studySessionModel.StackName
        );
    }
}