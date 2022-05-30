using System;
using System.Collections.Generic;
using System.Text;

namespace TSP.DataManager
{
   public class HomePageAttachmentManager: BaseObject
    {
       public static Permission GetUserPermission(int UserId, UserType ut)
       {
           return BaseObject.GetUserPermission(UserId, ut, TableType.HomePageAttachmentManager);
       }

       public static Permission GetUserPermissionVideos(int UserId, UserType ut)
       {
           return BaseObject.GetUserPermission(UserId, ut, TableType.Videos);
       }
       public static Permission GetUserPermissionPodcasts(int UserId, UserType ut)
       {
           return BaseObject.GetUserPermission(UserId, ut, TableType.Podcast);
       }

       public static Permission GetUserPermissionAmoozeshFiles(int UserId, UserType ut)
       {
           return BaseObject.GetUserPermission(UserId, ut, TableType.AmoozeshFiles);
       }

       protected override void InitAdapter()
       {
          
       }

       public override System.Data.DataTable DataTable
       {
           get
           {
               if ((this._dataTable == null))
               {

                  // this._dataTable = new DataManager.AccountingDataSet.AccountingAccTypeDataTable();
                   this.DataSet.Tables.Add(this._dataTable);
               }

               return this._dataTable;
           }
       }
   }
}
