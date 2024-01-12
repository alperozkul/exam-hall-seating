# Exam Hall Seating Arrangement System
> [!NOTE]
> It was developed for educational purposes within the scope of the graduation project.

### Description
This project encompasses a class placement application developed using ASP.NET Web App technology. The project aims to place students into specific classes based on student number or random.

### Motivation
1. **Paper Waste:** The student list is printed on paper.
2. **Labor:** The teachers in charge of the exam hang the student list sheet on the door of the relevant classes.
3. **Crowd:** Students crowd to look at the paper. There are students who cannot see the paper because of the crowd
4. **Stress:** Because the desks are not numbered, students have difficulty finding their seats and stress occurs.
5. **Time Waste:** A great deal of time is lost during the above-mentioned processes.

### Used Libraries
|Library|Description|     
|----|-----|   
|[Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)|Entity Framework (EF) Core is a lightweight, extensible, open source and cross-platform version of the popular Entity Framework data access technology.|
|[ASP.NET Core Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-8.0&tabs=visual-studio)|Manages users, passwords, profile data, roles, claims, tokens, email confirmation, and more.|
|[AutoMapper](https://automapper.org/)|AutoMapper is a simple little library built to solve a deceptively complex problem - getting rid of code that mapped one object to another. |
|[X.PagedList](https://github.com/dncuug/X.PagedList)|PagedList is a library that enables you to easily take an IEnumerable/IQueryable, chop it up into "pages", and grab a specific "page" by an index.|
|[ExcelDataReader](https://github.com/ExcelDataReader/ExcelDataReader)|Lightweight and fast library written in C# for reading Microsoft Excel files|
|[iTextSharp](https://github.com/itext/itextsharp)|Library that allows you to CREATE, ADAPT, INSPECT and MAINTAIN documents in the Portable Document Format (PDF), allowing you to add PDF functionality to your software projects with ease.|

### Features
- **CRUD Operations:**  Perform Create, Read, Update, and Delete operations for Students, Teachers, Courses, and Exams.

- **Assignment of Courses:** Assign courses to both Students and Teachers.

- **Class and Classroom Plan Creation:** Create and manage Classes and their classroom plans.

- **Exam Seating Arrangement:** Generate seating arrangements for exams by selecting a class and algorithm. Algorithms operate based on student number.
  - **Available Algorithms:**
    1. **Random:** Assign students randomly.    
    2. **Sort By Success:** Assign students based on their success levels.
    3. **Sort By Entry Year:** Assign students by sorting them according to their entry years.
    4. **Sort By Entry Year With Success:** Assign students by considering both their entry years and success levels.
       
- **Data Import and Export:** Read data from Excel, create PDF files, and send emails.

### How to Use
1. Clone the repository.
2. Set up your database and connection string in the *appsettings.json* file.
3. Run the migrations to create the database.
4. Configure *Seed.cs* file.
5. Check [Gmail SMTP](https://support.google.com/a/answer/176600?hl=en) for the mail sending process. Configure the following fields in the *MailService.cs* file.
   
   ```csharp
        smtpClient.Port = 587;
        smtpClient.UseDefaultCredentials = false;
        smtpClient.Credentials = new NetworkCredential("Mail", "AppPassword");
        smtpClient.EnableSsl = true;

        MailMessage mailMessage = new MailMessage();
        mailMessage.From = new MailAddress("Mail");
    ```
6. Build and run the project.


### Flowchart
The flowchart for class creation and exam seating arrangement is shown.


<img src="https://github.com/alperozkul/exam-hall-seating/assets/56310045/b25a4700-98fd-49ca-a056-7efd91f332e0" width="600"/>


### Conclusion
In conclusion, this project targets a more efficient and less stressful exam week experience in schools. By addressing issues in the manual system, such as exam list preparation and distribution, it aims to reduce stress and improve success rates. The effort to minimize paper usage further contributes to environmental sustainability. The successful implementation of these measures enhances both the efficiency of exam processes and their positive impact on the environment.
