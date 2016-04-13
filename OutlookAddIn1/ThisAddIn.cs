﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace OutlookAddIn1
{
    public partial class ThisAddIn
    {
        Outlook.NameSpace outlookNameSpace;
        Outlook.MAPIFolder inbox;
        Outlook.Items items;

        string connectionString = "Data Source=DESKTOP-ABEPKAT;Initial Catalog=Costco;Integrated Security=False;User ID=sa;Password=G4indigo;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            outlookNameSpace = this.Application.GetNamespace("MAPI");
            inbox = outlookNameSpace.GetDefaultFolder(
                    Microsoft.Office.Interop.Outlook.
                    OlDefaultFolders.olFolderInbox);

            items = inbox.Items;
            items.ItemAdd +=
                new Outlook.ItemsEvents_ItemAddEventHandler(items_ItemAdd);

            string a = DateTime.Now.AddDays(10).ToString();

//            string body = @"<html><head></head><body><div id='Header'><div><table border='0' cellpadding='0' cellspacing='0' width='100%'><tr><td width='100%' style='word-wrap:break-word'><table cellpadding='2' cellspacing='3' border='0' width='100%'><tr><td width='1%' nowrap='nowrap'><img src='http://q.ebaystatic.com/aw/pics/logos/ebay_95x39.gif' height='39' width='95' alt='eBay'></td><td align='left' valign='bottom'><span style='font-weight:bold; font-size:xx-small; font-family:verdana, sans-serif; color:#666'><b>eBay sent this message to Zhijun Ding (zjding2016).</b><br></span><span style='font-size:xx-small; font-family:verdana, sans-serif; color:#666'>Your registered name is included to show this message originated from eBay. <a href='http://pages.ebay.com/help/confidence/name-userid-emails.html'>Learn more</a>.</span></td></tr></table></td></tr></table></div></div><div id='Title'><div><table style='background-color:#ffe680' border='0' cellpadding='0' cellspacing='0' width='100%'><tr><td width='8' valign='top'><img src='http://q.ebaystatic.com/aw/pics/globalAssets/ltCurve.gif' height='8' width='8' alt=''></td><td valign='bottom' width='100%'><span style='font-weight:bold; font-size:14pt; font-family:arial, sans-serif; color:#000; margin:2px 0 2px 0'>Your item has been listed. Sell another item now!</span></td><td width='8' valign='top' align='right'><img src='http://p.ebaystatic.com/aw/pics/globalAssets/rtCurve.gif' height='8' width='8' alt=''></td></tr><tr><td style='background-color:#fc0' colspan='3' height='4'></td></tr></table></div></div><div id='SingleItemCTA'><div><table border='0' cellpadding='2' cellspacing='3' width='100%'><tr><td><font style='font-size:10pt; font-family:arial, sans-serif; color:#000'>Hi zjding2016,<table border='0' cellpadding='0' cellspacing='0' width='100%'><tr><td><img src='http://q.ebaystatic.com/aw/pics/s.gif' height='10' alt=' '></td></tr></table>Your item has been successfully listed on eBay. It may take some time for the item to appear on eBay search results. Here are the listing details:<table border='0' cellpadding='0' cellspacing='0' width='100%'><tr><td><img src='http://q.ebaystatic.com/aw/pics/s.gif' height='10' alt=' '></td></tr></table><div></div></font><div><table width='100%' cellpadding='0' cellspacing='3' border='0'><tr><td valign='top' align='center' width='100' nowrap='nowrap'><a href='http://rover.ebay.com/rover/0/e12000.m43.l1123/7?euid=db33b151a180449c92429caf42c24796&amp;loc=http%3A%2F%2Fcgi.ebay.com%2Fws%2FeBayISAPI.dll%3FViewItem%26item%3D152050500319%26ssPageName%3DADME%3AL%3ALCA%3AUS%3A1123'><img src='http://pics.ebaystatic.com/aw/pics/icon/iconPic_20x20.gif' alt='Cuisinart 14-Cup Programmable Coffeemaker' border='0'></a></td><td colspan='2' valign='top'><table width='100%' cellpadding='0' cellspacing='0' border='0'><tr><td style='font-size:10pt; font-family:arial, sans-serif; color:#000' colspan='2'><a href='http://rover.ebay.com/rover/0/e12000.m43.l1123/7?euid=db33b151a180449c92429caf42c24796&amp;loc=http%3A%2F%2Fcgi.ebay.com%2Fws%2FeBayISAPI.dll%3FViewItem%26item%3D152050500319%26ssPageName%3DADME%3AL%3ALCA%3AUS%3A1123'>Cuisinart 14-Cup Programmable Coffeemaker</a></td></tr><tr><td style='font-size:10pt; font-family:arial, sans-serif; color:#000' width='15%' nowrap='nowrap' valign='top'>Item Id:</td><td style='font-size:10pt; font-family:arial, sans-serif; color:#000' valign='top'>152050500319</td></tr><tr><td style='font-size:10pt; font-family:arial, sans-serif; color:#000' width='15%' nowrap='nowrap' valign='top'>Price:</td><td style='font-size:10pt; font-family:arial, sans-serif; color:#000' valign='top'>$94.89</td></tr><tr><td style='font-size:10pt; font-family:arial, sans-serif; color:#000' width='15%' nowrap='nowrap' valign='top'>End time:</td><td style='font-size:10pt; font-family:arial, sans-serif; color:#000' valign='top'>May-11-16 10:49:05 PDT</td></tr><tr><td style='font-size:10pt; font-family:arial, sans-serif; color:#000' width='15%' nowrap='nowrap' valign='top'>Listing fees:</td><td style='font-size:10pt; font-family:arial, sans-serif; color:#000' valign='top'>0</td></tr><tr><td colspan='2'><font style='font-size:10pt; font-family:arial, sans-serif; color:#000'><a href='http://rover.ebay.com/rover/0/e12000.m43.l1125/7?euid=db33b151a180449c92429caf42c24796&amp;loc=http%3A%2F%2Fcgi5.ebay.com%2Fws2%2FeBayISAPI.dll%3FUserItemVerification%26%26item%3D152050500319%26ssPageName%3DADME%3AL%3ALCA%3AUS%3A1125'>Revise item</a>   |    <a href='http://rover.ebay.com/rover/0/e12000.m43.l1121/7?euid=db33b151a180449c92429caf42c24796&amp;loc=http%3A%2F%2Fmy.ebay.com%2Fws%2FeBayISAPI.dll%3FMyeBay%26%26CurrentPage%3DMyeBaySelling%26ssPageName%3DADME%3AL%3ALCA%3AUS%3A1121'>Go to My eBay</a></font></td></tr></table></td></tr></table></div><td valign='top' width='185'><div><span style='font-weight:bold; font-size:10pt; font-family:arial, sans-serif; color:#000'>Ready to List Your Next Item?</span><table border='0' cellpadding='0' cellspacing='0' width='100%'><tr><td><img src='http://q.ebaystatic.com/aw/pics/s.gif' height='4' alt=' '></td></tr></table><a href='http://rover.ebay.com/rover/0/e12000.m44.l1127/7?euid=db33b151a180449c92429caf42c24796&amp;loc=http%3A%2F%2Fcgi5.ebay.com%2Fws%2FeBayISAPI.dll%3FSellHub3%26ssPageName%3DADME%3AL%3ALCA%3AUS%3A1127' title='http://rover.ebay.com/rover/0/e12000.m44.l1127/7?euid=db33b151a180449c92429caf42c24796&amp;loc=http%3A%2F%2Fcgi5.ebay.com%2Fws%2FeBayISAPI.dll%3FSellHub3%26ssPageName%3DADME%3AL%3ALCA%3AUS%3A1127'><img src='http://p.ebaystatic.com/aw/pics/buttons/btnSellMore.gif' border='0' height='32' width='120'></img></a><br><span style='font-style:italic; font-size:8pt; font-family:arial, sans-serif; color:#000'>Click to list another item</span></div></td></td></tr></table><br></div></div><div id='OneClickUnsubscribe'><div><style>.cub-cwrp {display:block; border:1px solid #dedfde; font-family:arial, sans-serif; font-size:10pt; margin-bottom:20px}
//h3.cub - chd {
//            margin: 0px; padding: 5px; display: block; background:#e7e7e7; font-size:14px}
//.cub - ccnt { padding: 0px 10px 10px 5px; display: block}
//                ul.cub - ulst { margin: 0px 0px 0px 10px; padding: 0px 0px 0px 10px}
//                ul.cub - ulst li, ul.cub - ulst li.cub - licn { list - style:square outside none; margin: 0px; padding: 10px 0px 0px 0px; line - height:16px}
//.cub - ltxt {
//                color:#333; display:block}
//</ style >< div class='cub-cwrp'><h3 class='cub-chd'>Select your email preferences</h3><div class='cub-ccnt'><ul class='cub-ulst'><li><span class='cub-ltxt'><span>Want to reduce your inbox email volume? <a href = 'http://my.ebay.com/ws/eBayISAPI.dll?DigestEmail&amp;emailType=12000' > Receive this email as a daily digest</a>.</span><br><span>For other email digest options, go to<a href= 'http://my.ebay.com/ws/eBayISAPI.dll?MyEbayBeta&amp;CurrentPage=MyeBayNextNotificationPreferences' > Notification Preferences</a> in My eBay.</span><br></span></li><li><span class='cub-ltxt'><span>Don't want to receive this email? <a href='http://my.ebay.com/ws/eBayISAPI.dll?EmailUnsubscribe&amp;emailType=12000'>Unsubscribe from this email</a>.</span><br></span></li></ul></div></div></div></div><div id='Tips'></div><div id='RTMEducational'></div><div id='MST'><div><table style='border:1px solid #6b7b91' border='0' cellpadding='0' cellspacing='0' width='100%'><tr style='background-color:#c9d2dc' height='1'><td><img src='http://p.ebaystatic.com/aw/pics/securityCenter/imgShield_25x25.gif' height='25' width='25' alt='Marketplace Safety Tip' align='absmiddle'></td><td style='font-weight:bold; font-size:10pt; font-family:arial, sans-serif; color:#000' nowrap='nowrap' width='20%'>Marketplace Safety Tip</td><td><img src='http://p.ebaystatic.com/aw/pics/securityCenter/imgTabCorner_25x25.gif' height='25' width='25' alt='' align='absmiddle'></td><td background='http://q.ebaystatic.com/aw/pics/securityCenter/imgFlex_1x25.gif' height='1' width='80%'></td></tr><tr><td style='font-size:10pt; font-family:arial, sans-serif; color:#000' colspan='4'><ul style='margin-top: 5px; margin-bottom: 5px;'><li style='padding-bottom: 3px; line-height: 120%; padding-top: 3px; list-style-type: square;'>If you are contacted about buying a similar item outside of eBay, please do not respond. Outside-of-eBay transactions are against eBay policy, and they are not covered by eBay services such as feedback and eBay purchase protection programs.</li></ul></td></tr><tr><td style='background-color:#c9d2dc' colspan='4'><img src='http://q.ebaystatic.com/aw/pics/s.gif' height='1' width='1'></td></tr></table><br></div></div><div id='Footer'><div><hr style='HEIGHT: 1px'><table border='0' cellpadding='0' cellspacing='0' width='100%'><tr><td width='100%'><font style='font-size:8pt; font-family:arial, sans-serif; color:#000000'>Email reference id: [#db33b151a180449c92429caf42c24796#]</font></td></tr></table><br></div><hr style='HEIGHT: 1px'><table border='0' cellpadding='0' cellspacing='0' width='100%'><tr><td width='100%'><font style='font-size:xx-small; font-family:verdana; color:#666'><a href='http://pages.ebay.com/education/spooftutorial/index.html'>Learn More</a> to protect yourself from spoof (fake) emails.<br><br>eBay sent this email to you at zjding@outlook.com about your account registered on <a href='http://www.ebay.com'>www.ebay.com</a>.<br><br>eBay sends these emails based on the preferences you set for your account. To unsubscribe from this email, change your <a href='http://my.ebay.com/ws/eBayISAPI.dll?MyEbayBeta&amp;CurrentPage=MyeBayNextNotificationPreferences'>communication preferences</a>. Please note that it may take up to 10 days to process your request. Visit our <a href='http://pages.ebay.com/help/policies/privacy-policy.html'>Privacy Notice</a> and <a href='http://pages.ebay.com/help/policies/user-agreement.html'>User Agreement</a> if you have any questions.<br><br>Copyright © 2016 eBay Inc. All Rights Reserved. Designated trademarks and brands are the property of their respective owners. eBay and the eBay logo are trademarks of eBay Inc. eBay Inc. is located at 2145 Hamilton Avenue, San Jose, CA 95125.  </font></td></tr></table><img src='http://rover.ebay.com/roveropen/0/e12000/7?euid=db33b151a180449c92429caf42c24796&amp;' height='1' width='1'></div></body></html>";

            //            // ItemID
            //            int iItemId = body.IndexOf("Item Id:");
            //            int iStart = iItemId + 100;
            //            int iEnd = body.IndexOf("</td>", iStart);
            //            string id = body.Substring(iStart - 4, iEnd - iStart + 4);

            //            // ListPrice
            //            iItemId = body.IndexOf("Price:");
            //            iStart = iItemId + 95;
            //            iEnd = body.IndexOf("</td>", iStart);
            //            string price = body.Substring(iStart, iEnd - iStart);

            //            // EndTime
            //            iItemId = body.IndexOf("End time:");
            //            iStart = iItemId + 98;
            //            iEnd = body.IndexOf("</td>", iStart);
            //            string endTime = body.Substring(iStart-1, iEnd - iStart+1);
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            // Note: Outlook no longer raises this event. If you have code that 
            //    must run when Outlook shuts down, see http://go.microsoft.com/fwlink/?LinkId=506785
        }

        void items_ItemAdd(object Item)
        {
            string filter = "USED CARS";
            Outlook.MailItem mail = (Outlook.MailItem)Item;
            if (Item != null)
            {
                if (mail.TaskSubject.IndexOf("Your eBay listing is confirmed") == 0)
                {
                    string subject = mail.Subject;

                    string productName = subject.Substring(32, subject.Length - 32);

                    string sqlString = "select top 1 * from Archieve where name = '" + productName + "' order by ImportedDT desc";

                    string categoryID = "";

                    SqlConnection cn = new SqlConnection(connectionString);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    Product product = new Product();

                    cn.Open();
                    cmd.CommandText = sqlString;
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();

                        product.Name = Convert.ToString(reader["Name"]);
                        product.UrlNumber = Convert.ToString(reader["UrlNumber"]);
                        product.ItemNumber = Convert.ToString(reader["ItemNumber"]);
                        product.Category = Convert.ToString(reader["Category"]);
                        product.Price = Convert.ToDecimal(reader["Price"]);
                        product.Shipping = Convert.ToDecimal(reader["Shipping"]);
                        product.Discount = Convert.ToString(reader["Discount"]);
                        product.Details = Convert.ToString(reader["Details"]);
                        product.Specification = Convert.ToString(reader["Specification"]);
                        product.ImageLink = Convert.ToString(reader["ImageLink"]);
                        product.Url = Convert.ToString(reader["Url"]);
                    }
                    reader.Close();

                    string body = mail.HTMLBody;

                    // ItemID
                    int iItemId = body.IndexOf("Item Id:");
                    int iStart = iItemId + 100;
                    int iEnd = body.IndexOf("</td>", iStart);
                    string id = body.Substring(iStart - 4, iEnd - iStart + 4);

                    // ListPrice
                    iItemId = body.IndexOf("Price:");
                    iStart = iItemId + 95;
                    iEnd = body.IndexOf("</td>", iStart);
                    string price = body.Substring(iStart, iEnd - iStart);

                    // EndTime
                    iItemId = body.IndexOf("End time:");
                    iStart = iItemId + 98;
                    iEnd = body.IndexOf("</td>", iStart);
                    string endTime = body.Substring(iStart - 1, iEnd - iStart + 1);

                    //MessageBox.Show(id + "|" + price + "|" + endTime);

                    sqlString = "INSERT INTO eBay_CurrentListings (Name, eBayItemNumber, eBayListingPrice, " +
                                "eBayListingDT, CostcoUrlNumber, CostcoUrl, eBayDescription, ImageLink) " +
                                "VALUES ('" + product.Name + "', '" + id + "', '" + price + "', '" + DateTime.Now.AddDays(10).ToString() + "', '" +
                                product.UrlNumber + "', '" + product.Url + "', '" +
                                product.Specification + "', '" + product.ImageLink + "')";

                    cmd.CommandText = sqlString;
                    cmd.ExecuteNonQuery();

                    cn.Close();
                }
                else if (mail.TaskSubject.IndexOf("Relist") == 0)
                {
                    string subject = mail.Subject;

                    string productName = subject.Substring(7, subject.Length - 7);

                    SqlConnection cn = new SqlConnection(connectionString);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;

                    Product product = new Product();

                    cn.Open();

                    string sqlString = "DELETE FROM eBay_CurrentListings WHERE Name = '" + productName + "'";
                    cmd.CommandText = sqlString;
                    cmd.ExecuteNonQuery();

                    cn.Close();
                }
            } 
        }

        void Inspectors_NewInspector(Microsoft.Office.Interop.Outlook.Inspector Inspector)
        {
            Outlook.MailItem mailItem = Inspector.CurrentItem as Outlook.MailItem;
            if (mailItem != null)
            {
                if (mailItem.EntryID == null)
                {
                    mailItem.Subject = "This text was added by using code";
                    mailItem.Body = "This text was added by using code";
                }

            }
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
