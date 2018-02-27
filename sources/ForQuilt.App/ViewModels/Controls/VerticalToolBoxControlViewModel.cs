//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------

using System.Windows.Ink;
using System.Windows.Input;
using ForQuilt.App.Commands.WorkArea.Drawing;
using ForQuilt.App.Commands.WorkArea.Selecting;
using ForQuilt.App.Models;
using ForQuilt.App.Models.Strokes;

namespace ForQuilt.App.ViewModels.Controls
{
    internal class VerticalToolBoxControlViewModel
    {
        public VerticalToolBoxControlViewModel()
        {
            DrawCommand = new WorkAreaDrawCommand();
            SelectByLassoCommand = new WorkAreaSelectByLassoCommand();
            SelectByRectangleCommand = new WorkAreaSelectByRectangleCommand();
            EraseCommand = new WorkAreaEraseCommand();
            ChangeColorCommand = new WorkAreaChangeColorCommand();
            DrawLineCommand = new WorkAreaDrawShapeCommand<LineStroke>(ModelStorage.DrawLineModel);
            RepeateSelectionCommand = new WorkAreaRepeateSelectionCommand(ModelStorage.RepeateSelectionModel);
            DrawRectangleCommand = new WorkAreaDrawShapeCommand<Stroke>(ModelStorage.DrawRectangleModel);
            DrawEllipseCommand = new WorkAreaDrawShapeCommand<EllipseStroke>(ModelStorage.DrawEllipseModel);
            ChangeThicknessCommand = new WorkAreaChangeThicknessCommand();
            GroupDrawingsCommand = new WorkAreaGroupDrawingsCommand();
            UnGroupDrawingsCommand = new WorkAreaUnGroupDrawingsCommand();
        }


        public ICommand DrawCommand { get; set; }
        public ICommand SelectByLassoCommand { get; set; }
        public ICommand SelectByRectangleCommand { get; set; }
        public ICommand EraseCommand { get; set; }
        public ICommand ChangeColorCommand { get; set; }
        public ICommand ChangeThicknessCommand { get; set; }
        public ICommand DrawLineCommand { get; set; }
        public ICommand RepeateSelectionCommand { get; set; }
        public ICommand DrawRectangleCommand { get; set; }
        public ICommand DrawEllipseCommand { get; set; }
        public ICommand GroupDrawingsCommand { get; set; }
        public ICommand UnGroupDrawingsCommand { get; set; }
    }
}
