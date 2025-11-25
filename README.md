# How to Highlight Line Color While Saving Data as Stream in WPF EditControl
The EditControl is a powerful text editor that supports syntax highlighting, code editing, and advanced customization features. In certain scenarios, you may want to preserve line-specific formatting, such as custom colors, when saving the content to a stream for storage or further processing.

This example demonstrates how to update the line modification state in the Syncfusion WPF EditControl after saving data to a stream. By default, EditControl tracks line states such as Modified or Saved, which can be useful for indicating changes in the editor. In scenarios where you save the content programmatically, you may want to reset these states to reflect that the lines are now saved.

## Key features
The sample shows how to:
- Handle Ctrl+S to trigger a save operation.
- Write the editor content to a file.
- Use reflection to invoke the internal ChangeLinesState method to update line states.

## Code Example
```C#

_editor.PreviewKeyUp += OnPreviewKeyUp;
_editor.PreviewKeyDown += OnPreviewKeyDown;

private void OnPreviewKeyUp(object sender, KeyEventArgs e)
{
    if (e.Key == Key.RightCtrl || e.Key == Key.LeftCtrl)
        _isCtrlDown = false;
}

private void OnPreviewKeyDown(object sender, KeyEventArgs e)
{
    if (e.Key == Key.RightCtrl || e.Key == Key.LeftCtrl)
        _isCtrlDown = true;

    if (e.Key == Key.S && _isCtrlDown)
    {
        SetFileText(_editor.Text);
        e.Handled = true;
    }
}

private void SetFileText(string text)
{
    using (StreamWriter sw = new StreamWriter(_filePath))
    {
        sw.Write(text);

        // Use reflection to update line states
        MethodInfo methodInfo = _editor.GetType().GetMethod("ChangeLinesState", BindingFlags.NonPublic | BindingFlags.Instance);
        methodInfo?.Invoke(_editor, new object[] { LineModificationState.Modified, LineModificationState.Saved });
    }
}
```

## Reference
For complete implementation details and code snippets, refer to the official Syncfusion Knowledge Base article:  
[How-to-highlight-line-color-while-saving-the-data-as-stream-in-WPF-EditControl](https://www.syncfusion.com/kb/12173/how-to-highlight-the-line-color-while-saving-the-data-as-stream-in-wpf-editcontrol)
