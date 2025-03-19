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
            // Generate certificate HTML template with enhanced design
            return $@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset='utf-8' />
                <title>Course Completion Certificate</title>
                <style>
                    @import url('https://fonts.googleapis.com/css2?family=Montserrat:wght@400;600;700&family=Playfair+Display:wght@700&display=swap');
                    
                    body {{
                        font-family: 'Montserrat', sans-serif;
                        text-align: center;
                        margin: 0;
                        padding: 20px;
                        background-color: #f0f0f0;
                    }}
                    
                    .certificate-container {{
                        width: 850px;
                        height: 600px;
                        margin: 0 auto;
                        background: linear-gradient(135deg, #ffffff 0%, #f9f9ff 100%);
                        border: 1px solid #ccc;
                        padding: 40px;
                        position: relative;
                        box-shadow: 0 20px 40px rgba(0, 0, 0, 0.1);
                        overflow: hidden;
                    }}
                    
                    .border-design {{
                        position: absolute;
                        top: 0;
                        left: 0;
                        right: 0;
                        bottom: 0;
                        border: 2px solid #0066cc;
                        margin: 15px;
                        pointer-events: none;
                        z-index: 1;
                    }}
                    
                    .corner-ornament {{
                        position: absolute;
                        width: 60px;
                        height: 60px;
                        border: 3px solid #0066cc;
                        border-radius: 0;
                        z-index: 1;
                    }}
                    
                    .top-left {{
                        top: 15px;
                        left: 15px;
                        border-right: none;
                        border-bottom: none;
                    }}
                    
                    .top-right {{
                        top: 15px;
                        right: 15px;
                        border-left: none;
                        border-bottom: none;
                    }}
                    
                    .bottom-left {{
                        bottom: 15px;
                        left: 15px;
                        border-right: none;
                        border-top: none;
                    }}
                    
                    .bottom-right {{
                        bottom: 15px;
                        right: 15px;
                        border-left: none;
                        border-top: none;
                    }}
                    
                    .background-graphic {{
                        position: absolute;
                        top: 0;
                        left: 0;
                        right: 0;
                        bottom: 0;
                        opacity: 0.05;
                        z-index: 0;
                        pointer-events: none;
                        background: url('data:image/svg+xml;utf8,<svg xmlns=""http://www.w3.org/2000/svg"" width=""400"" height=""400"" viewBox=""0 0 800 800""><g transform=""scale(0.8)""><circle cx=""400"" cy=""400"" r=""300"" fill=""none"" stroke=""%230066cc"" stroke-width=""2""/><circle cx=""400"" cy=""400"" r=""250"" fill=""none"" stroke=""%230066cc"" stroke-width=""2""/><path d=""M 400,100 400,700 M 100,400 700,400"" stroke=""%230066cc"" stroke-width=""2""/></g></svg>');
                        background-repeat: no-repeat;
                        background-position: center;
                        background-size: 120% 120%;
                    }}
                    
                    .logo {{
                        position: relative;
                        margin-bottom: 10px;
                        font-family: 'Playfair Display', serif;
                        font-size: 24px;
                        letter-spacing: 3px;
                        color: #0066cc;
                        z-index: 2;
                    }}
                    
                    .logo-accent {{
                        font-weight: bold;
                        color: #002266;
                    }}
                    
                    .certificate-header {{
                        margin-bottom: 20px;
                        z-index: 2;
                        position: relative;
                    }}
                    
                    .certificate-title {{
                        font-family: 'Playfair Display', serif;
                        font-size: 48px;
                        font-weight: bold;
                        color: #0066cc;
                        margin-bottom: 10px;
                        text-transform: uppercase;
                        letter-spacing: 2px;
                        text-shadow: 1px 1px 1px rgba(0,0,0,0.1);
                    }}
                    
                    .certificate-subtitle {{
                        font-size: 20px;
                        margin-bottom: 20px;
                        color: #555;
                        font-weight: 400;
                    }}
                    
                    .recipient {{
                        font-size: 36px;
                        font-weight: 600;
                        margin: 20px 0;
                        color: #002266;
                        font-family: 'Playfair Display', serif;
                        position: relative;
                        z-index: 2;
                        text-transform: capitalize;
                    }}
                    
                    .recipient::after {{
                        content: '';
                        display: block;
                        width: 180px;
                        margin: 8px auto 0;
                        border-bottom: 3px solid #0066cc;
                    }}
                    
                    .certificate-subtitle-2 {{
                        font-size: 18px;
                        margin: 25px 0 10px;
                        color: #555;
                    }}
                    
                    .course-name {{
                        font-size: 26px;
                        font-weight: 600;
                        margin-bottom: 25px;
                        color: #0066cc;
                        padding: 0 60px;
                        position: relative;
                        z-index: 2;
                    }}
                    
                    .date-section {{
                        margin-top: 40px;
                        position: relative;
                        z-index: 2;
                    }}
                    
                    .date {{
                        font-size: 16px;
                        margin-bottom: 5px;
                        color: #666;
                    }}
                    
                    .signature-section {{
                        display: flex;
                        justify-content: space-around;
                        width: 80%;
                        margin: 40px auto 0;
                        position: relative;
                        z-index: 2;
                    }}
                    
                    .signature {{
                        text-align: center;
                        width: 200px;
                    }}
                    
                    .signature-line {{
                        width: 100%;
                        border-top: 1px solid #333;
                        margin-bottom: 5px;
                    }}
                    
                    .signature-name {{
                        font-weight: 600;
                        font-size: 16px;
                    }}
                    
                    .signature-title {{
                        font-size: 14px;
                        color: #666;
                    }}
                    
                    .seal {{
                        position: absolute;
                        bottom: 70px;
                        right: 60px;
                        width: 120px;
                        height: 120px;
                        display: flex;
                        align-items: center;
                        justify-content: center;
                        font-weight: bold;
                        color: #0066cc;
                        transform: rotate(-15deg);
                        z-index: 2;
                        background: url('data:image/svg+xml;utf8,<svg xmlns=""http://www.w3.org/2000/svg"" width=""120"" height=""120"" viewBox=""0 0 120 120""><circle cx=""60"" cy=""60"" r=""50"" fill=""none"" stroke=""%230066cc"" stroke-width=""2""/><circle cx=""60"" cy=""60"" r=""45"" fill=""none"" stroke=""%230066cc"" stroke-width=""1""/><circle cx=""60"" cy=""60"" r=""40"" fill=""none"" stroke=""%230066cc"" stroke-width=""1""/></svg>');
                        background-repeat: no-repeat;
                    }}
                    
                    .seal-text {{
                        font-family: 'Playfair Display', serif;
                        font-size: 14px;
                        transform: rotate(15deg);
                        text-align: center;
                        line-height: 1.2;
                    }}
                    
                    .certificate-footer {{
                        position: absolute;
                        bottom: 30px;
                        left: 0;
                        right: 0;
                        text-align: center;
                        font-size: 12px;
                        color: #666;
                        z-index: 2;
                    }}
                    
                    .id-number {{
                        font-family: monospace;
                        letter-spacing: 1px;
                    }}
                </style>
            </head>
            <body>
                <div class='certificate-container'>
                    <div class='background-graphic'></div>
                    <div class='border-design'></div>
                    <div class='corner-ornament top-left'></div>
                    <div class='corner-ornament top-right'></div>
                    <div class='corner-ornament bottom-left'></div>
                    <div class='corner-ornament bottom-right'></div>
                    
                    <div class='logo'><span class='logo-accent'>GALACTIC</span> UNIVERSITY</div>
                    
                    <div class='certificate-header'>
                        <div class='certificate-title'>Certificate</div>
                        <div class='certificate-subtitle'>of Achievement</div>
                    </div>
                    
                    <div class='certificate-subtitle'>This is to certify that</div>
                    <div class='recipient'>{userName}</div>
                    
                    <div class='certificate-subtitle-2'>has successfully completed the course</div>
                    <div class='course-name'>{courseTitle}</div>
                    
                    <div class='date-section'>
                        <div class='date'>Issued on {issueDate:MMMM d, yyyy}</div>
                    </div>
                    
                    <div class='signature-section'>
                        <div class='signature'>
                            <div class='signature-line'></div>
                            <div class='signature-name'>Prof. Teodor Nedelchev</div>
                            <div class='signature-title'>Course Director</div>
                        </div>
                        
                        <div class='signature'>
                            <div class='signature-line'></div>
                            <div class='signature-name'>Dr. Teodor Nedelchev</div>
                            <div class='signature-title'>University President</div>
                        </div>
                    </div>
                    
                    <div class='seal'>
                        <div class='seal-text'>GALACTIC<br>UNIVERSITY<br>OFFICIAL</div>
                    </div>
                    
                    <div class='certificate-footer'>
                        <div class='id-number'>Certificate ID: GU-{issueDate:yyyyMMdd}-{new Random().Next(1000, 9999)}</div>
                    </div>
                </div>
            </body>
            </html>";
        }
    }
}