using ItCommerce.Business.Entities;
using ItCommerce.Business.Extra;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using ZXing.Aztec.Internal;
using static Utils.IwajuTech.Business.Factories.NetworkFactory;

namespace Utils.IwajuTech.Business.Factories
{
    public class NetworkFactory
    {
        public IConfiguration config;
        private string _senderAccount = "admin@gmail.com";
        private string _senderPassword = "password";
        private string _Hoststmp = "password";



        private void setSenderParams(string _senderEmail, string _senderPword, string _Hoststmp) 
        {
            _senderAccount = _senderEmail; _senderPassword = _senderPword;
            _Hoststmp = _Hoststmp;
        }

        //method to send email to Gmail
        public bool sendEMailThroughGmail(string receiver,
            string fileAttached, string mailBody, string mailSubject)
        {
            //for account params
            string sender = _senderAccount;
            string senderPassword = _senderPassword;

            try
            {
                //Mail Message
                MailMessage mM = new MailMessage();
                //Mail Address
                mM.From = new MailAddress(sender);
                //receiver email id
                mM.To.Add(receiver);
                //subject of the email
                mM.Subject = mailSubject;
                //deciding for the attachment
                if (!fileAttached.Equals(string.Empty))  mM.Attachments.Add(new Attachment(fileAttached));
                //add the body of the email
                mM.Body = mailBody;
                mM.IsBodyHtml = true;
                //SMTP client
                SmtpClient sC = new SmtpClient("smtp.gmail.com");
                //port number for Gmail mail
                sC.Port = 587;
                //credentials to login in to Gmail account
                sC.Credentials = new NetworkCredential(sender, senderPassword);
                //enabled SSL
                sC.EnableSsl = true;
                //Send an email
                sC.Send(mM);

                return true;
            }//end of try block
            catch (Exception ex)
            {
                throw ex;
            }//end of catch
        }//end of Email Method


        /// <summary>
        /// method to send email to a list of receiver with a list of files attached
        /// </summary>
        /// <param name="emailList"></param>
        /// <param name="filesAttached"></param>
        /// <param name="mailBody"></param>
        /// <param name="mailSubject"></param>
        /// <returns></returns>
        public  bool sendEMailThroughGmailImproved(string[] emailList, string[] filesAttached, string mailBody, string mailSubject)
        {
            //for account params
            string sender = _senderAccount;
            string senderPassword = _senderPassword;

            try
            {
                //Mail Message
                MailMessage mM = new MailMessage();
                //Mail Address
                mM.From = new MailAddress(sender);

                //receiver email id
                foreach (string itemReceiver in emailList)
                {
                    mM.To.Add(itemReceiver);
                }

                //subject of the email
                mM.Subject = mailSubject;

                //deciding for the attachment
                foreach (string itemFile in filesAttached)
                {
                    mM.Attachments.Add(new Attachment(itemFile));                    
                }

                //add the body of the email
                mM.Body = mailBody;
                mM.IsBodyHtml = true;
                //SMTP client
                SmtpClient sC = new SmtpClient("smtp.gmail.com");
                //port number for Gmail mail
                sC.Port = 587;
                //credentials to login in to Gmail account
                sC.Credentials = new NetworkCredential(sender, senderPassword);
                //enabled SSL
                sC.EnableSsl = true;
                //Send an email
                sC.Send(mM);

                return true;
            }//end of try block
            catch (Exception ex)
            {
                throw ex;
            }//end of catch
        }//fin sendEMailThroughGmailImproved


        public static string SendEmail(string to, string subject, string body )
            {

            try
            {
                var mailE = new MimeMessage();
                mailE.From.Add(MailboxAddress.Parse("sandra.ward@ethereal.email"));
                mailE.To.Add(GroupAddress.Parse(to));
                //mailE.To.Add(MailboxAddress.Parse(to));
                mailE.Subject = subject;
                mailE.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

                using var stmp = new MailKit.Net.Smtp.SmtpClient();
                stmp.Connect("smtp.ethereal.email", 587, MailKit.Security.SecureSocketOptions.StartTls);
                //stmp.Authenticate("sandra.ward@ethereal.email", "sbFzbKmPPS9hm5aMBH");
                //stmp.Send(mailE);
                //stmp.Disconnect(true);

                return subject;
            }
            catch (Exception ex)
            {
                throw ex;
            }

                
            }






        public static string SendRegisterEmail(string to, string subject, string body, string username, string token)
        {

            try
            {
                string message = "<html>" +
                    "<style>\r\n.validation:hover {\r\n            background-color: #2C75FF!important;\r\n        }\r\n    </style>" +
                    "<body style=\"background-color:  #EAEDED \">" +
                        "<h1 style =\"font-size:70px;text-align:center\">Validation de votre compte sur Ichowo</h1>" +
                                "<h2 style =\"text-align:center;\"><span style=\"color:#000\">Bienvenu </span></h2> " +
                "<h5 style =\"text-align:center;\">Vous avez créé un compte sur la plateforme Ishowo</h5> <h5 style =\"text-align:center;\">Veuillez cliquer sur le bouton Verifier mon compte ci-dessous pour valider votre compte. Votre login par défaut est :<span style='color:#03044a' > " + username + "</span></h5> <h5 style =\"text-align:center;\"><button class=\"validation\"  style=\"padding:10px; border-radius:5px; background-color:#ff4901; color:#ffffff; text-decoration:none; text-align:center;\">   <a href=\"http://localhost:4200/#/auth/verification/" + token + "\" style=\"color: #FFFFFF; text-decoration:none; font-weight:bold\">Vérifier mon compte </a></button></h5>" +
                "<div class=\"container\" style=\"background-color:#03044a; padding:2px 20px 20px 20px\">" +
                "<h1 style=\"color:#00d072; text-align:center\">Nous contacter</h1>" +
                "<a style=\"color: #FFFFFF;\">Téléphone: <span style=\"font-weight:bold\">+229 61185747</span></a> <span style=\"color: #FFFFFF; float:right\">Adresse email: <a href=\"https://www.trinitycfx.com/\" style=\"color: #FFFFFF; text-decoration:none; font-weight:bold\">https://www.trinitycfx.com</a></span>" +
                "</div>" +
                "</div>" +
                            "</div>" +
                            "<div style=\"display:inline\">" +
                                "<br>" +
                                "<a style=\"text-decoration:none\" href=\"https://web.facebook.com/TrinityFinances\">facebook</a> <a href=\"https://www.youtube.com/channel/UCVqrtGa73gJ4Ue3e9fIwvRg/\" style=\"float:right; text-decoration:none\">youtube</a>" +
                            "</div >" +
                            "<p style=\"color:#10076d; text-align:center\" > Trinity Finance, Expert en change d'Argent et en Paiements</p> <p style=\"color:#10076d; text-align:center\" >Se rendre sur notre page | Se désinscrire </p>" +
                        "</div>" + "</body>" + "</html>";
                var mailE = new MimeMessage();
                mailE.From.Add(MailboxAddress.Parse("sandra.ward@ethereal.email"));
                mailE.To.Add(GroupAddress.Parse(to));
                //mailE.To.Add(MailboxAddress.Parse(to));
                mailE.Subject = subject;
                mailE.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

                using var stmp = new MailKit.Net.Smtp.SmtpClient();
                stmp.Connect("smtp.ethereal.email", 587, MailKit.Security.SecureSocketOptions.StartTls);
                //stmp.Authenticate("sandra.ward@ethereal.email", "sbFzbKmPPS9hm5aMBH");
                //stmp.Send(mailE);
                //stmp.Disconnect(true);

                return subject;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

    }

}

 

