using TMPro;
using UnityEngine;
using System.Collections;


public class BookContents : MonoBehaviour
{
    [TextArea(10, 20)][SerializeField] private string content;
    [Space][SerializeField] private TMP_Text leftSide;
    [SerializeField] private TMP_Text rightSide;
    [Space][SerializeField] private TMP_Text leftPagination;
    [SerializeField] private TMP_Text rightPagination;

    [Header("Page Turn Effect")]
    [SerializeField] private Color flashColor = Color.red;
    [SerializeField] private float flashDuration = 0.2f;

    private Color originalColor;

    private void Awake()
    {
        SetupContent();
        UpdatePagination();
        originalColor = leftSide.color;
    }

    private void SetupContent()
    {
        leftSide.text = content;
        rightSide.text = content;
    }

    private void UpdatePagination()
    {
        leftPagination.text = leftSide.pageToDisplay.ToString();
        rightPagination.text = rightSide.pageToDisplay.ToString();
    }

    public void PreviousPage()
    {
        if (leftSide.pageToDisplay < 1)
        {
            leftSide.pageToDisplay = 1;
            return;
        }

        if (leftSide.pageToDisplay - 2 > 1)
            leftSide.pageToDisplay -= 2;
        else
            leftSide.pageToDisplay = 1;

        rightSide.pageToDisplay = leftSide.pageToDisplay + 1;

        UpdatePagination();
        StartCoroutine(PageTurnFlash());
    }

    public void NextPage()
    {
        if (rightSide.pageToDisplay >= rightSide.textInfo.pageCount)
            return;

        if (leftSide.pageToDisplay >= leftSide.textInfo.pageCount - 1)
        {
            leftSide.pageToDisplay = leftSide.textInfo.pageCount - 1;
            rightSide.pageToDisplay = leftSide.pageToDisplay + 1;
        }
        else
        {
            leftSide.pageToDisplay += 2;
            rightSide.pageToDisplay = leftSide.pageToDisplay + 1;
        }

        UpdatePagination();
        StartCoroutine(PageTurnFlash());
    }

    private IEnumerator PageTurnFlash()
    {
        // Change both pages to flash color
        leftSide.color = flashColor;
        rightSide.color = flashColor;

        yield return new WaitForSeconds(flashDuration);

        // Revert to original color
        leftSide.color = originalColor;
        rightSide.color = originalColor;
    }
}