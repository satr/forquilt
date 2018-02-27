//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
namespace ForQuilt.App.Commands
{
    internal abstract class ImageFileOperationCommandBase: RegisteredCommandBase
    {
        protected const string Filter = "Jpeg (*.jpg)|*.jpg|Png (*.png)|*.png|Bmp (*.bmp)|*.bmp";
    }
}