// Source: https://www.reddit.com/r/Unity3D/comments/3akps3/change_color_of_text_and_image_on_a_button/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonTextTransition : UnityEngine.UI.Button
{
    TMP_Text text;

    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        Color color;
        switch (state)
        {
            case Selectable.SelectionState.Normal:
                color = this.colors.normalColor;
                break;
            case Selectable.SelectionState.Highlighted:
                color = this.colors.highlightedColor;
                break;
            case Selectable.SelectionState.Pressed:
                color = this.colors.pressedColor;
                break;
            case Selectable.SelectionState.Disabled:
                color = this.colors.disabledColor;
                break;
            default:
                color = this.colors.normalColor;
                break;
        }
        if (base.gameObject.activeInHierarchy)
        {
            switch (this.transition)
            {
                case Selectable.Transition.ColorTint:
                    ColorTween(color * this.colors.colorMultiplier, instant);
                    break;
            }
        }
    }

    private void ColorTween(Color targetColor, bool instant)
    {
        if (this.targetGraphic == null)
        {
            this.targetGraphic = this.image;
        }
        if (text == null)
        {
            text = GetComponentInChildren<TMP_Text>();
        }

        base.image.CrossFadeColor(targetColor, (!instant) ? this.colors.fadeDuration : 0f, true, true);
        text.CrossFadeColor(targetColor, (!instant) ? this.colors.fadeDuration : 0f, true, true);
        
    }
}