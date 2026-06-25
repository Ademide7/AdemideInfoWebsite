using System;
using System.Collections.Generic;
using System.Text;

namespace AdemideInfoWebsite.Application.Utilities;

// generate email template and body using string interpolation replace the placeholders with the actual values.
// use colour email template with a header, body and footer.
// The header should contain the subject of the email, the body should contain the message and the footer should contain the company name and contact information.
// The email template should be responsive and look good on all devices. The email template should be in HTML format and use inline CSS for styling.
// The email template should be compatible with all major email clients. The email template should be tested using Litmus or Email on Acid to ensure compatibility with all major email clients. The email template should be optimized for deliverability and avoid spam filters.
// The email template should be accessible and follow WCAG 2.1 guidelines. The email template should be easy to read and understand.
// The email template should be visually appealing and use appropriate images and graphics. The email template should be consistent with the company's branding and style guide.
// The email template should include a clear call to action and encourage the recipient to take action. Name of the website executively designed at the top using css.
public static class EmailTemplateAndBody
{
    public static string GenerateEmailTemplate(string subject, string message, string companyName, string contactInfo)
    {
        return $@"
        <html>
        <head>
            <style>
                body {{
                    font-family: Arial, sans-serif;
                    margin: 0;
                    padding: 0;
                    background-color: #f4f4f4;
                }}
                .email-container {{
                    max-width: 600px;
                    margin: auto;
                    background-color: #ffffff;
                    padding: 20px;
                    border-radius: 10px;
                }}
                .header {{
                    background-color: #007BFF;
                    color: #ffffff;
                    padding: 10px;
                    text-align: center;
                    border-top-left-radius: 10px;
                    border-top-right-radius: 10px;
                }}
                .body {{
                    padding: 20px;
                }}
                .footer {{
                    background-color: #f4f4f4;
                    color: #333333;
                    padding: 10px;
                    text-align: center;
                    border-bottom-left-radius: 10px;
                    border-bottom-right-radius: 10px;
                }}
                a {{
                    color: #007BFF;
                }}
            </style>
        </head>
        <body>
            <div class='email-container'>
                <div class='header'>
                    <h1>{subject}</h1>
                </div>
                <div class='body'>
                    <p>{message}</p>
                </div>
                <div class='footer'>
                    <p>{companyName} | {contactInfo}</p>
                </div>
            </div>
        </body>
        </html>";
    }

    // Register email template method that takes in the subject, message, company name and contact information and returns the email template.
    public static string GenerateRegisterEmailTemplate(string username, string email,string contactInfo)
    {
        string subject = "Welcome to Ademide Info Website!";
        string message = $"Hello {username},<br><br>Thank you for registering with us. Your email address is {email}.<br><br>We are excited to have you on board!<br><br>Best regards,<br>The Ademide Info Website Team";
        string companyName = "Ademide Info Website";  
        return GenerateEmailTemplate(subject, message, companyName, contactInfo);
    }

    // Password reset email template method that takes in the subject, message, company name and contact information and returns the email template. using otp.
    public static string GeneratePasswordResetEmailTemplate(string username, string fakePassword, string contactInfo)
    {
        string subject = "Password Reset Request";
        string message = $"Hello {username},<br><br>We received a request to reset your password. Your temporary password is: <strong>{fakePassword}</strong><br><br>Please use this password to log in and change your password immediately.<br><br>If you did not request a password reset, please contact us immediately.<br><br>Best regards,<br>The Ademide Info Website Team";
        string companyName = "Ademide Info Website";
        return GenerateEmailTemplate(subject, message, companyName, contactInfo);
    }

    // Update profile data email template method that takes in the subject, message, company name and contact information and returns the email template.
    public static string GenerateUpdateProfileEmailTemplate(string username, string contactInfo)
    {
        string subject = "Profile Update Confirmation";
        string message = $"Hello {username},<br><br>Your profile information has been successfully updated.<br><br>If you did not make this change, please contact us immediately.<br><br>Best regards,<br>The Ademide Info Website Team";
        string companyName = "Ademide Info Website";
        return GenerateEmailTemplate(subject, message, companyName, contactInfo);
    }

    // password change confirmation email template method that takes in the subject, message, company name and contact information and returns the email template.
    public static string GeneratePasswordChangeConfirmationEmailTemplate(string username, string contactInfo)
    {
        string subject = "Password Change Confirmation";
        string message = $"Hello {username},<br><br>Your password has been successfully changed.<br><br>If you did not make this change, please contact us immediately.<br><br>Best regards,<br>The Ademide Info Website Team";
        string companyName = "Ademide Info Website";
        return GenerateEmailTemplate(subject, message, companyName, contactInfo);
    }

    // Appointment confirmation email template method that takes in the subject, message, company name and contact information and returns the email template.
    public static string GenerateAppointmentConfirmationEmailTemplate(string username, string appointmentDate, string appointmentTime, string contactInfo)
    {
        string subject = "Appointment Confirmation";
        string message = $"Hello {username},<br><br>Your appointment has been confirmed for {appointmentDate} at {appointmentTime}.<br><br>We look forward to seeing you then!<br><br>Best regards,<br>The Ademide Info Website Team";
        string companyName = "Ademide Info Website";
        return GenerateEmailTemplate(subject, message, companyName, contactInfo);
    }

    public static string GenerateAppointmentReminderEmailTemplate(string username, string appointmentDate, string appointmentTime, string contactInfo)
    {
        string subject = "Appointment Reminder";
        string message = $"Hello {username},<br><br>This is a friendly reminder that you have an upcoming appointment on {appointmentDate} at {appointmentTime}.<br><br>We look forward to seeing you then!<br><br>Best regards,<br>The Ademide Info Website Team";
        string companyName = "Ademide Info Website";
        return GenerateEmailTemplate(subject, message, companyName, contactInfo);
    }



}

