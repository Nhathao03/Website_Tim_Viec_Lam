using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TimViec.Models;
using TimViec.Repository;
using TimViec.Respository;
using TimViec.ViewModel;
using IronPdf;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Components.RenderTree;

namespace TimViec.Controllers
{
    public class ManageCVController : Controller
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly ITypeCVRepository _typeCVRepository;
        private readonly ISectionRespository _sectionRespository;
        private readonly ICVsRepository _cvRepository;
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IServiceProvider _serviceProvider;
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageCVController(ITemplateRepository templateRepository,
            ITypeCVRepository typeCVRepository,
            ISectionRespository sectionRespository,
            ICVsRepository cVsRepository,
            IRazorViewEngine razorViewEngine,
            IWebHostEnvironment webHostEnvironment, 
            IServiceProvider serviceProvider,
            UserManager<ApplicationUser> userManager)
        {
            _templateRepository = templateRepository;
            _typeCVRepository = typeCVRepository;
            _sectionRespository = sectionRespository;
            _cvRepository = cVsRepository;
            _userManager = userManager;
            _razorViewEngine = razorViewEngine;
            _webHostEnvironment = webHostEnvironment;
            _serviceProvider = serviceProvider;
        }

        //Get all CV by UserID
        [HttpGet]
        [Authorize(Roles = "User")]
        public IActionResult ManageCV()
        {
            var getUser = _userManager.GetUserAsync(User);
            var getallCV = _cvRepository.ManageCV(getUser.Result.Id);       
            return View(getallCV);
        }

        //Watch CV
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetCVbyID(int id)
        {
            var renderCV = _cvRepository.GetTemplates_by_ID_CV(id);
            var sectionList = new List<Get_CV_ByCvid_ViewModelResult>();
            foreach (var item in renderCV)
            {
                sectionList.Add(new Get_CV_ByCvid_ViewModelResult()
                {
                    Id = item.Id,
                    CVID = item.CViD,
                    TypeID = item.TypeID,
                    Background = item.Background,
                    TypeName = item.TypeName,
                    Content = !string.IsNullOrEmpty(item.ContentJson) ? JsonConvert.DeserializeObject<dynamic>(item.ContentJson) : null,
                    Style = !string.IsNullOrEmpty(item.StyleJson) ? JsonConvert.DeserializeObject<dynamic>(item.StyleJson) : null,
                });
            }
            ViewBag.Background = sectionList.First().Background;
            return View(sectionList);
        }

        //Get form update CV
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetUpdateCV(int id)
        {
            var renderCV = _cvRepository.GetTemplates_by_ID_CV(id);
            var sectionList = new List<Get_CV_ByCvid_ViewModelResult>();
            foreach (var item in renderCV)
            {
                sectionList.Add(new Get_CV_ByCvid_ViewModelResult()
                {
                    Id = item.Id,
                    CVID = item.CViD,
                    TypeID = item.TypeID,
                    Background = item.Background,
                    TypeName = item.TypeName,
                    NameCV = item.NameCV,
                    Content = !string.IsNullOrEmpty(item.ContentJson) ? JsonConvert.DeserializeObject<dynamic>(item.ContentJson) : null,
                    Style = !string.IsNullOrEmpty(item.StyleJson) ? JsonConvert.DeserializeObject<dynamic>(item.StyleJson) : null,
                });
            }
            ViewBag.Background = sectionList.First().Background;
            return View(sectionList);
        }
      
        //Update CV
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateCV([FromBody] SaveCVRequest request)
        {
            try
            {
                var getCV = await _cvRepository.GetByIdAsync(request.cvid);

                if (request != null)
                {

                    if (request.cvid == getCV.Id && getCV.IsDefault == true)
                    {
                        // Start update Section Avatar
                        // convert request to object insert
                        // serilize object
                        // update to database
                        var getsectionAvatar = await _sectionRespository.GetByIdAsync(request.Avatar.SectionID);
                        if(request.Avatar.Url != null)
                        {
                            var updateAvatar = new SectionAvatarInsert()
                            {
                                Url = request.Avatar.Url,
                            };
                            var updateAvatarSer = JsonConvert.SerializeObject(updateAvatar);
                            getsectionAvatar.ContentJson = updateAvatarSer;
                            await _sectionRespository.UpdateAsync(getsectionAvatar);
                        }
                        // End update Section Avatar

                        //************************************************************************************************************************************
                        // Start update Section Username
                        // convert request to object insert
                        // serilize object
                        // update to database
                        var SectionUsername = await _sectionRespository.GetByIdAsync(request.Username.SectionID);
                        if(SectionUsername != null)
                        {
                            var UpdateUsername = new SectionUserInsert()
                            {
                                Name = request.Username.Name,
                            };
                            var updateUsernameSer = JsonConvert.SerializeObject(UpdateUsername);
                            SectionUsername.ContentJson = updateUsernameSer;
                            await _sectionRespository.UpdateAsync(SectionUsername);
                        }

                        // End update Section Username

                        //************************************************************************************************************************************
                        // Start update Section Title
                        // convert request to object insert
                        // serilize object
                        // update to database
                        var SectionTitle = await _sectionRespository.GetByIdAsync(request.Title.SectionID);
                        var insertTitle = new SectionTitleInsert()
                        {
                            Job_position = request.Title.Job_position,
                        };
                        var insertTitleSer = JsonConvert.SerializeObject(insertTitle);
                        SectionTitle.ContentJson = insertTitleSer;
                        await _sectionRespository.UpdateAsync(SectionTitle);
                        // End update Section Title

                        //************************************************************************************************************************************
                        // Start update Section Career
                        // convert request to object insert
                        // serilize object
                        // update to database
                        var SectionCareer = await _sectionRespository.GetByIdAsync(request.Career.SectionID);
                        var insertCareer = new SectionCareerInsert()
                        {
                            Career_goal = request.Career.Career_goal,
                        };
                        var insertCareerSer = JsonConvert.SerializeObject(insertCareer);
                        SectionCareer.ContentJson = insertCareerSer;                           
                        await _sectionRespository.UpdateAsync(SectionCareer);
                        // End update Section Career

                        //************************************************************************************************************************************
                        // Start update Section Profile
                        // convert request to object insert
                        // serilize object
                        // update to database
                        var SectionProfile = await _sectionRespository.GetByIdAsync(request.Profile.SectionID);
                        var insertProfile = new SectionProfileInsert()
                        {
                            Email = request.Profile.Email,
                            Phone = request.Profile.Phone,
                            Website = request.Profile.Website,
                            Address = request.Profile.Address,
                        };
                        var insertProfileSer = JsonConvert.SerializeObject(insertProfile);
                        SectionProfile.ContentJson = insertProfileSer;
                        await _sectionRespository.UpdateAsync(SectionProfile);
                        // End update Section Profile

                        //************************************************************************************************************************************
                        // Start update Section Education
                        // convert request to object insert
                        // serilize object
                        // update to database
                        var SectionEducation = await _sectionRespository.GetByIdAsync(request.Educations.First().SectionID);
                        var insertEducation = request.Educations.Select(x => new SectionEducationInsert()
                        {
                            Institution = x.Institution,
                            Major = x.Major,
                            EndYear = x.EndYear,
                            Grade = x.Grade,
                            StartYear = x.StartYear,
                        });

                        var insertEducationSer = JsonConvert.SerializeObject(insertEducation);
                        SectionEducation.ContentJson = insertEducationSer;
                        await _sectionRespository.UpdateAsync(SectionEducation);
                        // End update Section Education

                        //************************************************************************************************************************************
                        // Start Insert Section Skill
                        // convert request to object insert
                        // serilize object
                        // insert to database
                        var SectionSkill = await _sectionRespository.GetByIdAsync(request.Skills.First().SectionID);
                        var insertSkill = request.Skills.Select(x => new SectionSkillInsert()
                        {
                            Name = x.Name,
                            Description = x.Description,
                        });

                        var insertSkillSer = JsonConvert.SerializeObject(insertSkill);
                        SectionSkill.ContentJson = insertSkillSer;
                        await _sectionRespository.UpdateAsync(SectionSkill);
                        // End Insert Section Skill

                        //************************************************************************************************************************************
                        // Start Insert Section Experience
                        // convert request to object insert
                        // serilize object
                        // insert to database
                        var SectionExperience = await _sectionRespository.GetByIdAsync(request.Experiences.First().SectionID);
                        var insertExperience = request.Experiences.Select(x => new SectionExperienceInsert()
                        {
                            Company = x.Company,
                            StartDate = x.StartDate,
                            EndDate = x.EndDate,
                            Position = x.Position,
                            Responsibilities = x.Responsibilities,
                        });

                        var insertExperienceSer = JsonConvert.SerializeObject(insertExperience);
                        SectionExperience.ContentJson = insertExperienceSer;    
                        await _sectionRespository.UpdateAsync(SectionExperience);
                        // End Insert Section Experience

                        //************************************************************************************************************************************
                        // Start Insert Section Project
                        // convert request to object insert
                        // serilize object
                        // insert to database
                        var SectionProject = await _sectionRespository.GetByIdAsync(request.Projects.First().SectionID);
                        var insertProject = request.Projects.Select(x => new SectionProjectInsert()
                        {
                            From = x.From,
                            To = x.To,
                            Position = x.Position,
                            Team_size = x.Team_size,
                            Description = x.Description,
                            Project = x.Project,
                        });

                        var insertProjectSer = JsonConvert.SerializeObject(insertProject);
                        SectionProject.ContentJson = insertProjectSer;
                        await _sectionRespository.UpdateAsync(SectionProject);
                        // End Insert Section Project

                        //************************************************************************************************************************************
                        // Start Insert Section Certification
                        // convert request to object insert
                        // serilize object
                        // insert to database
                        var SectionCertification = await _sectionRespository.GetByIdAsync(request.Certifications.First().SectionID);
                        var insertCertification = request.Certifications.Select(x => new SectionCertificationInsert()
                        {
                            Name = x.Name,
                            Year = x.Year,
                        });

                        var insertCertificationSer = JsonConvert.SerializeObject(insertCertification);
                        SectionCertification.ContentJson = insertCertificationSer;
                        await _sectionRespository.UpdateAsync(SectionCertification);
                        // End Insert Section Certification

                        //************************************************************************************************************************************
                        // Start Insert Section Activities
                        // convert request to object insert
                        // serilize object
                        // insert to database
                        var SectionActivities = await _sectionRespository.GetByIdAsync(request.Activities.First().SectionID);
                        var insertActivities = request.Activities.Select(x => new SectionActivitiesInsert()
                        {
                            Activity = x.Activity,
                            StartDate = x.StartDate,
                            EndDate = x.EndDate,
                            Position = x.Position,
                            Description = x.Description
                        });

                        var insertActivitiesSer = JsonConvert.SerializeObject(insertActivities);
                        SectionActivities.ContentJson = insertActivitiesSer;
                        await _sectionRespository.UpdateAsync(SectionActivities);
                        // End Insert Section insertActivities

                        //************************************************************************************************************************************
                        // Start Insert Section Honor
                        // convert request to object insert
                        // serilize object
                        // insert to database
                        var SectionHonor = await _sectionRespository.GetByIdAsync(request.Honor_Awards.First().SectionID);
                        var insertHonor = request.Honor_Awards.Select(x => new SectionHonor_awardInsert()
                        {
                            Year = x.Year,
                            Name = x.Name,
                        });

                        var insertHonorSer = JsonConvert.SerializeObject(insertHonor);
                        SectionHonor.ContentJson = insertHonorSer;
                        await _sectionRespository.UpdateAsync(SectionHonor);
                        // End Insert Section insert Honor

                        //************************************************************************************************************************************
                        // Start Insert Section Interest
                        // convert request to object insert
                        // serilize object
                        // insert to database
                        var SectionInterest = await _sectionRespository.GetByIdAsync(request.Interests.First().SectionID);
                        var insertInterest = request.Interests.Select(x => new SectionInterestInsert()
                        {
                            Description = x.Description,
                        });

                        var insertInterestSer = JsonConvert.SerializeObject(insertInterest);
                        SectionInterest.ContentJson = insertInterestSer;
                        await _sectionRespository.UpdateAsync(SectionInterest);
                        // End Insert Section insert Interest

                        //************************************************************************************************************************************
                        // Start Insert Section Refer
                        // convert request to object insert
                        // serilize object
                        // insert to database
                        var SectionRefer = await _sectionRespository.GetByIdAsync(request.References.First().SectionID);
                        var insertRefer = request.References.Select(x => new SectionReferencesInsert()
                        {
                            Description = x.Description,
                        });

                        var insertReferSer = JsonConvert.SerializeObject(insertRefer);
                        SectionRefer.ContentJson = insertReferSer;
                        await _sectionRespository.UpdateAsync(SectionRefer);
                        // End Insert Section insert Refer

                        //************************************************************************************************************************************
                        // Start Insert Section Additional
                        // convert request to object insert
                        // serilize object
                        // insert to database
                        var Sectioninformation = await _sectionRespository.GetByIdAsync(request.information.SectionID);
                        var insertinformation = new SectionInformationInsert()
                        {
                            Description = request.information.Description
                        };
                        var insertinformationSer = JsonConvert.SerializeObject(insertinformation);
                        Sectioninformation.ContentJson = insertinformationSer;
                        await _sectionRespository.UpdateAsync(Sectioninformation);
                        // End Insert Section Additional
                    }
                }
                return Json(new { Success = true });
            }
            catch
            {
                return Json(new { Success = false });
            }
        }

        //Delete CV
        public async Task<IActionResult> DeleteCV(int id)
        {
            await _cvRepository.DeleteAsync(id);
            return RedirectToAction("ManageCV");
        }


        //Export CV to PDF
        [HttpGet]
        public async Task<IActionResult> GetCVAsPdf(int id)
        {
            var renderCV = _cvRepository.GetTemplates_by_ID_CV(id);
            var sectionList = new List<Get_CV_ByCvid_ViewModelResult>();

            foreach (var item in renderCV)
            {
                sectionList.Add(new Get_CV_ByCvid_ViewModelResult()
                {
                    Id = item.Id,
                    CVID = item.CViD,
                    TypeID = item.TypeID,
                    Background = item.Background,
                    TypeName = item.TypeName,
                    Content = !string.IsNullOrEmpty(item.ContentJson) ? JsonConvert.DeserializeObject<dynamic>(item.ContentJson) : null,
                    Style = !string.IsNullOrEmpty(item.StyleJson) ? JsonConvert.DeserializeObject<dynamic>(item.StyleJson) : null,
                });
            }

            // Render Razor view to HTML string
            string htmlContent = await RenderRazorViewToStringAsync("ExportPdf", sectionList);

            // Generate PDF using IronPDF
            var htmlToPdf = new HtmlToPdf();
            htmlToPdf.PrintOptions.MarginTop = 0;
            htmlToPdf.PrintOptions.MarginBottom = 0;
            htmlToPdf.PrintOptions.MarginLeft = 0;
            htmlToPdf.PrintOptions.MarginRight = 0;
            var pdfDocument = htmlToPdf.RenderHtmlAsPdf(htmlContent);

            return File(pdfDocument.BinaryData, "application/pdf", $"CV_{id}.pdf");
        }
        //Export CV to PDF
        private async Task<string> RenderRazorViewToStringAsync(string viewName, object model)
        {
            // Set the model for the view
            ViewData.Model = model;

            using (var stringWriter = new StringWriter())
            {
                // Find the view
                var viewResult = _razorViewEngine.FindView(ControllerContext, viewName, false);

                if (!viewResult.Success)
                {
                    throw new FileNotFoundException($"View {viewName} not found.");
                }

                // Create the view context
                var viewContext = new ViewContext(
                    ControllerContext,
                    viewResult.View,
                    ViewData,
                    TempData,
                    stringWriter,
                    new HtmlHelperOptions()
                );

                // Render the view to a string
                await viewResult.View.RenderAsync(viewContext);
                return stringWriter.ToString();
            }
        }
    }
}

