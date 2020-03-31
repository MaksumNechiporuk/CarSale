using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CarSale.Entities
{
    public class DBObject
    {
        public static void Initial(DBContext content)
        {
            //if (!content.CarSale.Any())
            //{
            //    content.AddRange(
            //       new BlogModel
            //       {
            //           Name= "Resume vs Cover Letter : Main Differences Between",
            //           PrewText= "Do you know that a resume and a cover letter actually differ? Even though both of these documents are essential when applying for a job, it is critical that you know how to differentiate them in order to write them for the right purpose. Resume vs cover letter should be properly defined so that you",
            //           Img = "resume-vs-cover-letter.jpg",
            //           FullText= "Do you know that a resume and a cover letter actually differ? Even though both of these documents are essential when applying for a job, it is critical that you know how to differentiate them in order to write them for the right purpose. Resume vs cover letter should be properly defined so that you understand what aim each of them has. When writing either a resume or a cover letter, you should demonstrate your skills and competencies in order to prove to the employer or HR manager that you are worth being considered for the position. Ideally, both a resume and a cover letter should pinpoint that you have skills that are relevant and corresponding to the job opening. As you start working on the resume or a cover letter, be sure that you should come to realize the resume vs cover letter structure and mode or organization. Moreover, it is essential to adhere to the template and follow the general requirements as well as the resume cover letter format.First of all, a resume and a cover letter are not the same and each of them has a different structure and purpose. Second, you need to make sure that you do not repeat the information from the resume in the cover letter. These should be complementary documents that focus on your different angles.One of the biggest differences is that a cover letter is optional and is not always required from a job applicant. However, if you attach a cover letter, your chances of being invited for an interview may increase. The major difference between cover letter and resume is that the resume focuses on the main points concerning your educational background and work experience, soft and hard skills, talents and achievements, as well as your address, contact information, obtained academic degrees, etc. On the other hand, a cover letter provides a more detailed introduction of your persona to the employer. The core aim of writing a cover letter is to establish a connection with your target readers. You should as well focus on how the company will benefit from having you on their team. All in all, both of the documents are equally important in the job application process. At the same time, if they are poorly written and if the structure and requirements are not followed, they can also make a negative impression on the employer. Therefore, be sure that you adhere to the proper writing style, business etiquette, formatting, and other criteria.",
            //           Author="Banana",
            //           Date=DateTime.Now.Date
            //       },
            //         new BlogModel
            //         {
            //             Name = "60 BEST IDEAS ON NURSING RESEARCH PAPER TOPICS",
            //             PrewText = "Students at medical universities should work even more than other students. They deal with human health and have to write their pieces of research on complicated issues. However, there is a separate group of medical staff – nurses. Usually, they get their degree at college or universities. Besides, often, they should combine their study and",
            //             Img = "60-Nursing-Research-Paper-Topics.jpg",
            //             FullText = "Students at medical universities should work even more than other students. They deal with human health and have to write their pieces of research on complicated issues. However, there is a separate group of medical staff – nurses. Usually, they get their degree at college or universities. Besides, often, they should combine their study and work. Nurses come first to your help, even before a doctor. They recommend you how to cope with different types of injuries, illnesses, and give you the correct medical instruction and support you when you feel bad. Therefore, they should also do both – practice a lot and write some papers to pass their exams. However, according to the latest pieces of research, most nurses face a lot of problems because of boring nursing essay topics. Usually, it looks like you come to your professor, get the nursing paper topic and start to write it. Of course, you can choose your nursing research topic. However, many students face problems since they do not know how to select a nursing essay topic correctly. Therefore, we have collected and sorted out the most interesting latest nursing paper topics. Besides, as we have mentioned, we realize clearly that nurses mostly work and study at the same time. In this case, if you feel the lack of the time, strength, inspiration, and academic skills for writing papers even after reading this list of nursing essay topics – feel free to contact 123helpme.org. It is a writing service of professional and passionate writers who can write on different issues, including nursing paper topics. Furthermore, ordering your paper, you will have a chance to write it together with the writer since you are in touch 24\\7. You should realize that ordering your article does not mean you are a lousy student. It says that you use a writing service to save your time for some more important things. Besides, you may be sure that professional writers will make you an outstanding paper full of the most essential information. Thus, you will get the following advantages: 1) have more time on practicing in the hospital; private life, etc. 2) have good marks and the opportunity to continue your study, 3) learn theoretical materials quickly using professional writers’ help and experience. Below you will find approximately 60 latest nursing paper topics sorted in thematic groups. We highly encourage you to read them and choose one that fits you best.",
            //             Author = "Banana",
            //             Date = DateTime.Now.Date
            //         },
            //           new BlogModel
            //           {
            //               Name = "TYPES OF EXAM QUESTIONS",
            //               PrewText = "Since the examination is the most common tool, which is used to assess the students’ knowledge, various and sundry examination questions have been created through the history of education. This article not only gives a brief description of six widespread types of examination questions, but also suggests handy tips on implementing each of them. Exam",
            //               Img = "Types-of-Exam-Questions.jpg",
            //               FullText = "Since the examination is the most common tool, which is used to assess the students’ knowledge, various and sundry examination questions have been created through the history of education. This article not only gives a brief description of six widespread types of examination questions, but also suggests handy tips on implementing each of them. It goes without saying that each and every examination question demands logical application. Consisting merely from a statement, true/ false questions do not require much time and effort. What is needed is simply to mark whether the statement is true or false. No half-truths exist. This type of questions has a lot in common with multiple choice questions. For instance, it is used to barely check the familiarity with the general knowledge of the subject, as well as widespread misconceptions. It takes only a couple of seconds to read the statement and to respond. What is more, due to its simplicity and shortness, students can cover a substantial number of these questions during the examination time and examiners check the answers quickly. Nevertheless, it takes a stupendous amount of time to create the tasks.",
            //               Author = "Banana",
            //               Date = DateTime.Now.Date
            //           },
            //             new BlogModel
            //             {
            //                 Name = "TIPS FOR CREATING PERFECT DISCUSSION BOARD POST FOR YOUR ONLINE CLASSES",
            //                 PrewText = "Discussion forums have become an inseparable component of our communication realities. When we join online classrooms, we spend most of our time posting our stories and discussing the most important subjects and topics with instructors and peers. Definitely, online class discussion board provides perfect opportunities for online interactions and learning, so you need to know",
            //                 Img = "Tips-for-Creating-the-Perfect-Discussion-Post.jpg",
            //                 FullText = "Discussion forums have become an inseparable component of our communication realities. When we join online classrooms, we spend most of our time posting our stories and discussing the most important subjects and topics with instructors and peers. Definitely, online class discussion board provides perfect opportunities for online interactions and learning, so you need to know how to write a discussion board post. When you join an online course, you also expect that your discussion posts will be memorable and impressive. You want to stand out among your peers. You want to be popular. You want to receive positive feedback from your classmates. You also want to earn the highest grades for your discussion posts. Every post is graded, based on its quality and contents. Therefore, you must know how to produce a brilliant discussion post for your online classes. Now you need to learn how to write a discussion board post, and your discussion posts will boost your popularity among classmates. We have created this article to help you improve your academic results. Follow our recommendations, and you will see how your discussion posts impress your teacher and improve your image and reputation in the eyes of your classmates.",
            //                 Author = "Banana",
            //                 Date = DateTime.Now.Date
            //             }
            //    );
            //    content.SaveChanges();
            //}

        }
    }
}
