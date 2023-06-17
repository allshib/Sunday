﻿using System;
using DevExpress.ExpressApp.Blazor.Components.Models;

namespace Sunday.Blazor.Server.Editors.ButtonViewItem {
    public class ButtonModel : ComponentModelBase {
        public string Text {
            get => GetPropertyValue<string>();
            set => SetPropertyValue(value);
        }
        public void ClickFromUI() {
            Click?.Invoke(this, EventArgs.Empty);
        }
        public event EventHandler Click;
    }
}
