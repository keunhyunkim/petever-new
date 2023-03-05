using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SimpleTest : MonoBehaviour
{
    private const int HIGHEST = 30;
    private const int HIGH = 20;
    private const int LOW = 10;
    private const int LOWEST = 0;
    private const int QUEST_NUM = 5;
    public static int DEFAULT_TOTALSCORE = HIGHEST * QUEST_NUM;

    // The list of buttons in this group.
    public List<Button> buttons;

    // The index of the current selected button in this group.
    private int currentSelectionIndex = 0;

    // The score for this group.
    private int score;

    // The global variable to store the total score of all groups.
    public static int totalScore = DEFAULT_TOTALSCORE;

    // The sprite to use for the unselected buttons.
    public Sprite graySprite;

    // The sprite to use for the selected buttons.
    public Sprite yellowSprite;

    // Called when a button in this group is clicked.
    public void OnButtonClick(int buttonIndex)
    {
        // If the clicked button is already the current selection, do nothing.
        if (buttonIndex == currentSelectionIndex)
        {
            return;
        }

        // If there is a current selection, change its sprite back to the gray sprite.
        if (currentSelectionIndex >= 0)
        {
            buttons[currentSelectionIndex].image.sprite = graySprite;
            totalScore -= GetButtonValue(currentSelectionIndex);
        }

        // Set the current selection to the clicked button and change its sprite to the yellow sprite.
        currentSelectionIndex = buttonIndex;
        buttons[currentSelectionIndex].image.sprite = yellowSprite;

        // Update the score for this group.
        score = GetButtonValue(currentSelectionIndex);

        // Add the score for this group to the total score.
        totalScore += score;
    }

    // Returns the value of the button at the given index.
    private int GetButtonValue(int index)
    {
        switch (index)
        {
            case 0:
                return HIGHEST;
            case 1:
                return HIGH;
            case 2:
                return LOW;
            case 3:
                return LOWEST;
            default:
                return 0;
        }
    }
}
