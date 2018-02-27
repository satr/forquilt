//----------------------------------------------------------------------------
//  Copyright © 2013 ForQuilt.CodePlex.com
//  All rights reserved.       
//----------------------------------------------------------------------------
using System.ComponentModel;
using System.Windows.Input;
using ForQuilt.App.Annotations;
using ForQuilt.App.Commands.WorkArea.RotateImage;
using ForQuilt.App.Models;

namespace ForQuilt.App.ViewModels.Controls
{

    class RotationControlViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private decimal _rotationAngle = 0m;

        public RotationControlViewModel()
        {
            RotateLeft45 = new WorkAreaRotateLeftCommand(this, 45);
            RotateRight45 = new WorkAreaRotateRightCommand(this, 45);
            RotateLeft1 = new WorkAreaRotateLeftCommand(this, 1);
            RotateRight1 = new WorkAreaRotateRightCommand(this, 1);
            ResetRotation = new WorkAreaResetRotationCommand(this);
            ModelStorage.WorkAreaModel.OnSelectionChanged += (sender, args) => RotationAngle = 0m;
        }

        public ICommand RotateLeft45 { get; set; }
        public ICommand RotateRight45 { get; set; }
        public ICommand RotateLeft1 { get; set; }
        public ICommand RotateRight1 { get; set; }
        public ICommand ResetRotation { get; set; }
        
        public decimal RotationAngle
        {
            get
            {
                return _rotationAngle;
            }
            set
            {
                if (_rotationAngle == value)
                {
                    return;
                }
                _rotationAngle = value;
                OnPropertyChanged("RotationAngle");
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Update()
        {
            ModelStorage.WorkAreaModel.RotateSelected(RotationAngle);
        }
    }
}
