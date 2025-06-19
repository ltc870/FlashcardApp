using FlashcardApp.DTOs.FlashcardDTO;
using FlashcardApp.Models.Flashcard;

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
}