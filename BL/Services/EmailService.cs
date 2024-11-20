using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BL.Services.DTO;


namespace BL.Services
{
    public class EmailService
    {



        public EmailService()
        {

            
        }
        //public EmailService(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}
        public async Task<bool> SendEmailAsync(EmailDTO emailDto)
        {
            try
            {
                SmtpClient Smtp = new SmtpClient();

                Smtp.Host = "email-smtp.us-east-1.amazonaws.com";
                Smtp.Port = 587;
                Smtp.EnableSsl = true;
                Smtp.Timeout = 100000;
                Smtp.Credentials = new NetworkCredential("AKIAJARGVWG545ASXCKQ", "Am0jSW8xsZ/OaCWFPEuKUqvqh3U1fDH2iHlYRgVnkJwA");
                //bool success = false;

                var message = new MailMessage();
                if (!String.IsNullOrEmpty(emailDto.From) && (emailDto.From.EndsWith("@lightblueusa.com") || emailDto.From.EndsWith("@hmchelp.com")))
                {

                    //message.Sender = new MailAddress(emailDto.From, "LightBlue");//email.SentFrom
                    //message.From = new MailAddress(emailDto.From, "LightBlue");//email.SentFrom

                    message.Sender = new MailAddress("noreply@mcprogramming.net", "LightBlue");//email.SentFrom
                    message.From = new MailAddress("noreply@mcprogramming.net", "LightBlue");//email.SentFrom
                }
                else
                {

                    message.Sender = new MailAddress("noreply@mcprogramming.net", "LightBlue");//email.SentFrom
                    message.From = new MailAddress("noreply@mcprogramming.net", "LightBlue");//email.SentFrom
                }
                if (emailDto?.Bcc.Count > 0)
                {
                    foreach (var bcc in emailDto?.Bcc)
                    {

                        message.Bcc.Add(new MailAddress(bcc.ToString()));
                    }
                }
                if (emailDto?.Cc.Count > 0)
                {
                    foreach (var cc in emailDto.Cc)
                    {

                        message.CC.Add(new MailAddress(cc.ToString()));
                    }
                }
                foreach (var address in emailDto?.To)
                {
                    message.To.Add(address);
                    //message.To.Add("amarkowits@hmchelp.com");
                }
                message.Subject = emailDto.Subject;
                message.IsBodyHtml = true;

                string imgUrl = "https://lb.hmchelp.com/assets/img/logo2.png";
                var sig = $@"
                    <p style=""font-size: 16px;"">
                        <img src=""{imgUrl}"" alt=""Company Logo"" style=""width: 100px;"">
                    </p>
                    <p style=""font-size: 14px;"">
                        Best Regards,<br>
                        Rayhil 
                    </p>";

                emailDto.Body += "<br>";
                message.Body = emailDto.Body.Replace("\n", "<br>");
                message.Body += sig;


                if (emailDto.Attachment != null)
                {
                    using (WebClient wc = new WebClient())
                    {
                        while (wc.IsBusy)
                            await Task.Delay(100);
                        byte[] data = await wc.DownloadDataTaskAsync("https://lb.hmchelp.com" + emailDto.Attachment);
                        MemoryStream stream = new MemoryStream(data);
                        stream.Position = 0;
                        Attachment attachment = new Attachment(stream, "Attachment.pdf", "application/pdf");
                        message.Attachments.Add(attachment);
                    }
                }
                //if (emailDto.Attachments != null)
                //{
                //    foreach (var file in emailDto.Attachments)
                //    {
                //        FileService _fileService = new FileService(context, mapper, hostingEnvironment);
                //        var fileResult = await _fileService.GetFile(file.Id ?? 0);
                //        if (fileResult != null)
                //        {
                //            var (fileStream, contentType, fileName) = await ExtractFileInfo(fileResult);
                //            var attachmentItem = new Attachment(fileStream, fileName, contentType);
                //            message.Attachments.Add(attachmentItem);
                //        }
                //    }
                //}
                Smtp.Timeout = 100000;
                await Smtp.SendMailAsync(message);
                message = null;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        //private async Task<(Stream fileStream, string contentType, string fileName)> ExtractFileInfo(IActionResult fileResult)
        //{
        //    if (fileResult is FileStreamResult fsr)
        //    {
        //        var memoryStream = new MemoryStream();
        //        await fsr.FileStream.CopyToAsync(memoryStream);
        //        memoryStream.Position = 0;
        //        return (memoryStream, fsr.ContentType, fsr.FileDownloadName);
        //    }
        //    throw new InvalidOperationException("Invalid file result type.");
        //}
        public async Task<bool> sendVerifiyEmail(string token, string email, string name)
        {
            var verificationLink = "https://lb.hmchelp.com/login/setPassword?code=" + token;
            var userDisplayName = name;



            EmailDTO emailDto = new EmailDTO();
            List<string> to = [email];
            emailDto.To = to;
            emailDto.Subject = "Verify Your Email and Set Password";
            emailDto.Body = $@"
            <p>Hello {userDisplayName},</p>
            <p>Welcome to LightBlue! Please click the link below to verify your email address and set your password:</p>
            <p><a href=""{verificationLink}"">Verify Email and Set Password</a></p>
            <p>If you did not sign up for our service, please ignore this email.</p>
            <br>";
            return await SendEmailAsync(emailDto);
        }
    }
}
