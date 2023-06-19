using DevExpress.Persistent.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sunday.Module.BusinessObjects.Enums {
	public enum PhoneEnum {
		[ImageName("Upload_Cancel")]
		None,
        [ImageName("BO_Phone")]
        Mobile,
        [ImageName("State_Task_InProgress")]
        Work
	}
}
