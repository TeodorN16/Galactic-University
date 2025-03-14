using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticUniversity.Utility
{
    public class CertificateGenerator
    {
        public static string GenerateCertificateHtml(string userName, string courseTitle, DateTime issueDate)
        {
            // Generate certificate HTML template
            return $@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset='utf-8' />
                <title>Course Completion Certificate</title>
                <style>
                    body {{
                        font-family: 'Arial', sans-serif;
                        text-align: center;
                        margin: 0;
                        padding: 0;
                        background-color: #f9f9f9;
                    }}
                    .certificate-container {{
                        width: 800px;
                        height: 600px;
                        margin: 0 auto;
                        background-color: white;
                        border: 20px solid #0066cc;
                        padding: 30px;
                        position: relative;
                    }}
                    .logo {{
                        margin-bottom: 20px;
                    }}
                    .certificate-title {{
                        font-size: 48px;
                        font-weight: bold;
                        color: #0066cc;
                        margin-bottom: 20px;
                    }}
                    .certificate-subtitle {{
                        font-size: 24px;
                        margin-bottom: 30px;
                    }}
                    .recipient {{
                        font-size: 36px;
                        font-weight: bold;
                        margin-bottom: 30px;
                        color: #333;
                    }}
                    .course-name {{
                        font-size: 28px;
                        font-weight: bold;
                        margin-bottom: 40px;
                        color: #0066cc;
                    }}
                    .date {{
                        font-size: 18px;
                        margin-bottom: 40px;
                    }}
                    .signature {{
                        font-weight: bold;
                        margin-top: 20px;
                        border-top: 2px solid #333;
                        display: inline-block;
                        padding-top: 10px;
                    }}
                    .seal {{
                        position: absolute;
                        bottom: 30px;
                        right: 30px;
                        width: 100px;
                        height: 100px;
                        border: 2px solid #0066cc;
                        border-radius: 50%;
                        display: flex;
                        align-items: center;
                        justify-content: center;
                        font-weight: bold;
                        color: #0066cc;
                        transform: rotate(-15deg);
                    }}
                </style>
            </head>
            <body>
                <div class='certificate-container'>
                    <div class='logo'>GALACTIC UNIVERSITY</div>
                    <div class='certificate-title'>Certificate of Completion</div>
                    <div class='certificate-subtitle'>This is to certify that</div>
                    <div class='recipient'>{userName}</div>
                    <div class='certificate-subtitle'>has successfully completed the course</div>
                    <div class='course-name'>{courseTitle}</div>
                    <div class='date'>Issued on {issueDate:MMMM d, yyyy}</div>
                    <div class='signature'>University Director</div>
                    <div class='seal'>OFFICIAL</div>
                </div>
            </body>
            </html>";
        }
    }
}
