using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TimViec.Models;
using TimViec.Repository;
using TimViec.Respository;
using TimViec.ViewModel;


namespace TimViec.Controllers
{

    public class ToolsController : Controller
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly ITypeCVRepository _typeCVRepository;
        private readonly ISectionRespository _sectionRespository; 
        private readonly ICVsRepository _cvRepository;
    

        private readonly UserManager<ApplicationUser> _userManager;
        public ToolsController(ITemplateRepository templateRepository,
            ITypeCVRepository typeCVRepository,
            ISectionRespository sectionRespository,
            ICVsRepository cVsRepository,

            UserManager<ApplicationUser> userManager)
        {
            _templateRepository = templateRepository;
            _typeCVRepository = typeCVRepository;
            _sectionRespository = sectionRespository;
            _cvRepository = cVsRepository;
            _userManager = userManager;
        }
        // Get list template
        public async Task<IActionResult> ListTemplateCV()
        {
            var getAll_TemplateCV = _templateRepository.GetAllInformation_Table_Template_and_TypeCV();
            return View(getAll_TemplateCV);
        }

        [HttpGet]
        // Get template by category
        public async Task<IActionResult> Gettemplatebycategory(int category_id)
        {
            var getTemplate_by_category = _templateRepository.Get_Template_By_Categories(category_id);
            return View(getTemplate_by_category);

        }


        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> RenderCreateCV(int id)
        {
            var renderCV = _cvRepository.GetTemplates_by_ID_CV(id);
            var sectionList = new List<Get_CV_ByCvid_ViewModelResult>();
            var getUsername = await _userManager.GetUserAsync(User);
            foreach (var item in renderCV)
            {
                sectionList.Add(new Get_CV_ByCvid_ViewModelResult()
                {
                    Id = item.Id,
                    CVID = item.CViD,
                    TypeID = item.TypeID,
                    Background = item.Background,
                    UserName = getUsername.Fullname,
                    TypeName = item.TypeName,
                    Content = !string.IsNullOrEmpty(item.ContentJson) ? JsonConvert.DeserializeObject<dynamic>(item.ContentJson) : null,
                    Style = !string.IsNullOrEmpty(item.StyleJson) ? JsonConvert.DeserializeObject<dynamic>(item.StyleJson) : null,
                });
            }
            ViewBag.Background = sectionList.First().Background;
            return View(sectionList);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> SaveCV([FromBody] SaveCVRequest request)
        {
            try
            {
                var getCV = await _cvRepository.GetByIdAsync(request.cvid);

                if (request != null)
                {

                    if (request.cvid == getCV.Id && getCV.IsDefault == false)
                    {
                        //Get templateID to insert in CVs
                        var getTemplateID = _cvRepository.GetByIdAsync(request.cvid);
                        // Insert to table CVs
                        var getUser = await _userManager.GetUserAsync(User);
                        var newCV = new CV()
                        {
                            UserID = getUser.Id,
                            Title = request.NameCV,
                            CreateAt = DateTime.UtcNow,
                            UpdateAt = DateTime.UtcNow,
                            IsDefault = true,
                            templateId = getTemplateID.Result.templateId,
                        };
                        await _cvRepository.AddAsync(newCV);
                        // Start Insert Section Avatar
                        // convert request to object insert
                        // serilize object
                        // insert to database
                        var templateSectionAvatar = await _sectionRespository.GetByIdAsync(request.Avatar.SectionID);
                        if(request.Avatar.Url  != null)
                        {
                            var insertAvatar = new SectionAvatarInsert()
                            {
                                Url = request.Avatar.Url
                            };
                            var insertAvatarSer = JsonConvert.SerializeObject(insertAvatar);
                            var sectionAvatar = new Sections()
                            {
                                ContentJson = insertAvatarSer,
                                cvId = newCV.Id,
                                Name = templateSectionAvatar.Name,
                                Order = templateSectionAvatar.Order,
                                StyleJson = templateSectionAvatar.StyleJson,
                                TypeSectionId = templateSectionAvatar.TypeSectionId
                            };

                            await _sectionRespository.AddAsync(sectionAvatar);
                        }
                        else
                        {
                            var sectionAvatar = new Sections()
                            {
                                ContentJson = templateSectionAvatar.ContentJson,
                                cvId = newCV.Id,
                                Name = templateSectionAvatar.Name,
                                Order = templateSectionAvatar.Order,
                                StyleJson = templateSectionAvatar.StyleJson,
                                TypeSectionId = templateSectionAvatar.TypeSectionId
                            };

                            await _sectionRespository.AddAsync(sectionAvatar);
                        }
                        // End Insert Section Avatar

                        //************************************************************************************************************************************
                        // Start Insert Section Username
                        // convert request to object insert
                        // serilize object
                        // insert to database
                        var templateSectionUsername = await _sectionRespository.GetByIdAsync(request.Username.SectionID);
                        var insertUsername = new SectionUserInsert()
                        {
                            Name = request.Username.Name,
                        };
                        var insertUsernameSer = JsonConvert.SerializeObject(insertUsername);
                        var sectionUsername = new Sections()
                        {
                            ContentJson = insertUsernameSer,
                            cvId = newCV.Id,
                            Name = templateSectionUsername.Name,
                            Order = templateSectionUsername.Order,
                            StyleJson = templateSectionUsername.StyleJson,
                            TypeSectionId = templateSectionUsername.TypeSectionId
                        };

                        await _sectionRespository.AddAsync(sectionUsername);
                        // End Insert Section Username

                        //************************************************************************************************************************************
                        // Start Insert Section Title
                        // convert request to object insert
                        // serilize object
                        // insert to database
                        var templateSectionTitle = await _sectionRespository.GetByIdAsync(request.Title.SectionID);
                        var insertTitle = new SectionTitleInsert()
                        {
                            Job_position = request.Title.Job_position,
                        };
                        var insertTitleSer = JsonConvert.SerializeObject(insertTitle);
                        var sectionTitle = new Sections()
                        {
                            ContentJson = insertTitleSer,
                            cvId = newCV.Id,
                            Name = templateSectionTitle.Name,
                            Order = templateSectionTitle.Order,
                            StyleJson = templateSectionTitle.StyleJson,
                            TypeSectionId = templateSectionTitle.TypeSectionId
                        };

                        await _sectionRespository.AddAsync(sectionTitle);
                        // End Insert Section Title

                        //************************************************************************************************************************************
                        // Start Insert Section Career
                        // convert request to object insert
                        // serilize object
                        // insert to database
                        var templateSectionCareer = await _sectionRespository.GetByIdAsync(request.Career.SectionID);
                        var insertCareer = new SectionCareerInsert()
                        {
                            Career_goal = request.Career.Career_goal,
                        };
                        var insertCareerSer = JsonConvert.SerializeObject(insertCareer);
                        var sectionCareer = new Sections()
                        {
                            ContentJson = insertCareerSer,
                            cvId = newCV.Id,
                            Name = templateSectionCareer.Name,
                            Order = templateSectionCareer.Order,
                            StyleJson = templateSectionCareer.StyleJson,
                            TypeSectionId = templateSectionCareer.TypeSectionId
                        };

                        await _sectionRespository.AddAsync(sectionCareer);
                        // End Insert Section Career

                        //************************************************************************************************************************************
                        // Start Insert Section Profile
                        // convert request to object insert
                        // serilize object
                        // insert to database
                        var templateSectionProfile = await _sectionRespository.GetByIdAsync(request.Profile.SectionID);
                        var insertProfile = new SectionProfileInsert()
                        {
                            Email = request.Profile.Email,
                            Phone = request.Profile.Phone,
                            Website = request.Profile.Website,
                            Address = request.Profile.Address,
                        };
                        var insertProfileSer = JsonConvert.SerializeObject(insertProfile);
                        var sectionProfile = new Sections()
                        {
                            ContentJson = insertProfileSer,
                            cvId = newCV.Id,
                            Name = templateSectionProfile.Name,
                            Order = templateSectionProfile.Order,
                            StyleJson = templateSectionProfile.StyleJson,
                            TypeSectionId = templateSectionProfile.TypeSectionId
                        };

                        await _sectionRespository.AddAsync(sectionProfile);
                        // End Insert Section Profile

                        //************************************************************************************************************************************
                        // Start Insert Section Education
                        // convert request to object insert
                        // serilize object
                        // insert to database
                        var templateSectionEducation = await _sectionRespository.GetByIdAsync(request.Educations.First().SectionID);
                        var insertEducation = request.Educations.Select(x => new SectionEducationInsert()
                        {
                            Institution = x.Institution,
                            Major = x.Major,
                            EndYear = x.EndYear,
                            Grade = x.Grade,
                            StartYear = x.StartYear,
                        });

                        var insertEducationSer = JsonConvert.SerializeObject(insertEducation);
                        var sectionEducation = new Sections()
                        {
                            ContentJson = insertEducationSer,
                            cvId = newCV.Id,
                            Name = templateSectionEducation.Name,
                            Order = templateSectionEducation.Order,
                            StyleJson = templateSectionEducation.StyleJson,
                            TypeSectionId = templateSectionEducation.TypeSectionId
                        };

                        await _sectionRespository.AddAsync(sectionEducation);
                        // End Insert Section Education

                        //************************************************************************************************************************************
                        // Start Insert Section Skill
                        // convert request to object insert
                        // serilize object
                        // insert to database
                        var templateSectionSkill = await _sectionRespository.GetByIdAsync(request.Skills.First().SectionID);
                        var insertSkill = request.Skills.Select(x => new SectionSkillInsert()
                        {
                            Name = x.Name,
                            Description = x.Description,
                        });

                        var insertSkillSer = JsonConvert.SerializeObject(insertSkill);
                        var sectionSkill = new Sections()
                        {
                            ContentJson = insertSkillSer,
                            cvId = newCV.Id,
                            Name = templateSectionSkill.Name,
                            Order = templateSectionSkill.Order,
                            StyleJson = templateSectionSkill.StyleJson,
                            TypeSectionId = templateSectionSkill.TypeSectionId
                        };

                        await _sectionRespository.AddAsync(sectionSkill);
                        // End Insert Section Skill

                        //************************************************************************************************************************************
                        // Start Insert Section Experience
                        // convert request to object insert
                        // serilize object
                        // insert to database
                        var templateSectionExperience = await _sectionRespository.GetByIdAsync(request.Experiences.First().SectionID);
                        var insertExperience = request.Experiences.Select(x => new SectionExperienceInsert()
                        {
                            Company = x.Company,
                            StartDate = x.StartDate,
                            EndDate = x.EndDate,
                            Position = x.Position,
                            Responsibilities = x.Responsibilities,
                        });

                        var insertExperienceSer = JsonConvert.SerializeObject(insertExperience);
                        var sectionExperience = new Sections()
                        {
                            ContentJson = insertExperienceSer,
                            cvId = newCV.Id,
                            Name = templateSectionExperience.Name,
                            Order = templateSectionExperience.Order,
                            StyleJson = templateSectionExperience.StyleJson,
                            TypeSectionId = templateSectionExperience.TypeSectionId
                        };

                        await _sectionRespository.AddAsync(sectionExperience);
                        // End Insert Section Experience

                        //************************************************************************************************************************************
                        // Start Insert Section Project
                        // convert request to object insert
                        // serilize object
                        // insert to database
                        var templateSectionProject = await _sectionRespository.GetByIdAsync(request.Projects.First().SectionID);
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
                        var sectionProject = new Sections()
                        {
                            ContentJson = insertProjectSer,
                            cvId = newCV.Id,
                            Name = templateSectionProject.Name,
                            Order = templateSectionProject.Order,
                            StyleJson = templateSectionProject.StyleJson,
                            TypeSectionId = templateSectionProject.TypeSectionId
                        };
                        await _sectionRespository.AddAsync(sectionProject);
                        // End Insert Section Project

                        //************************************************************************************************************************************
                        // Start Insert Section Certification
                        // convert request to object insert
                        // serilize object
                        // insert to database
                        var templateSectionCertification = await _sectionRespository.GetByIdAsync(request.Certifications.First().SectionID);
                        var insertCertification = request.Certifications.Select(x => new SectionCertificationInsert()
                        {
                           Name = x.Name,
                           Year = x.Year,
                        });

                        var insertCertificationSer = JsonConvert.SerializeObject(insertCertification);
                        var sectionCertification = new Sections()
                        {
                            ContentJson = insertCertificationSer,
                            cvId = newCV.Id,
                            Name = templateSectionCertification.Name,
                            Order = templateSectionCertification.Order,
                            StyleJson = templateSectionCertification.StyleJson,
                            TypeSectionId = templateSectionCertification.TypeSectionId
                        };
                        await _sectionRespository.AddAsync(sectionCertification);
                        // End Insert Section Certification

                        //************************************************************************************************************************************
                        // Start Insert Section Activities
                        // convert request to object insert
                        // serilize object
                        // insert to database
                        var templateSectionActivities = await _sectionRespository.GetByIdAsync(request.Activities.First().SectionID);
                        var insertActivities = request.Activities.Select(x => new SectionActivitiesInsert()
                        {
                           Activity = x.Activity,
                           StartDate = x.StartDate,
                           EndDate = x.EndDate,
                           Position = x.Position,
                           Description = x.Description
                        });

                        var insertActivitiesSer = JsonConvert.SerializeObject(insertActivities);
                        var sectionActivities = new Sections()
                        {
                            ContentJson = insertActivitiesSer,
                            cvId = newCV.Id,
                            Name = templateSectionActivities.Name,
                            Order = templateSectionActivities.Order,
                            StyleJson = templateSectionActivities.StyleJson,
                            TypeSectionId = templateSectionActivities.TypeSectionId
                        };
                        await _sectionRespository.AddAsync(sectionActivities);
                        // End Insert Section insertActivities

                        //************************************************************************************************************************************
                        // Start Insert Section Honor
                        // convert request to object insert
                        // serilize object
                        // insert to database
                        var templateSectionHonor = await _sectionRespository.GetByIdAsync(request.Honor_Awards.First().SectionID);
                        var insertHonor = request.Honor_Awards.Select(x => new SectionHonor_awardInsert()
                        {
                            Year = x.Year,
                            Name = x.Name,
                        });

                        var insertHonorSer = JsonConvert.SerializeObject(insertHonor);
                        var sectionHonor = new Sections()
                        {
                            ContentJson = insertHonorSer,
                            cvId = newCV.Id,
                            Name = templateSectionHonor.Name,
                            Order = templateSectionHonor.Order,
                            StyleJson = templateSectionHonor.StyleJson,
                            TypeSectionId = templateSectionHonor.TypeSectionId
                        };
                        await _sectionRespository.AddAsync(sectionHonor);
                        // End Insert Section insert Honor

                        //************************************************************************************************************************************
                        // Start Insert Section Interest
                        // convert request to object insert
                        // serilize object
                        // insert to database
                        var templateSectionInterest = await _sectionRespository.GetByIdAsync(request.Interests.First().SectionID);
                        var insertInterest = request.Interests.Select(x => new SectionInterestInsert()
                        {
                            Description = x.Description,                           
                        });

                        var insertInterestSer = JsonConvert.SerializeObject(insertInterest);
                        var sectionInterest = new Sections()
                        {
                            ContentJson = insertInterestSer,
                            cvId = newCV.Id,
                            Name = templateSectionInterest.Name,
                            Order = templateSectionInterest.Order,
                            StyleJson = templateSectionInterest.StyleJson,
                            TypeSectionId = templateSectionInterest.TypeSectionId
                        };
                        await _sectionRespository.AddAsync(sectionInterest);
                        // End Insert Section insert Interest

                        //************************************************************************************************************************************
                        // Start Insert Section Refer
                        // convert request to object insert
                        // serilize object
                        // insert to database
                        var templateSectionRefer = await _sectionRespository.GetByIdAsync(request.References.First().SectionID);
                        var insertRefer = request.References.Select(x => new SectionReferencesInsert()
                        {
                            Description = x.Description,
                        });

                        var insertReferSer = JsonConvert.SerializeObject(insertRefer);
                        var sectionRefer = new Sections()
                        {
                            ContentJson = insertReferSer,
                            cvId = newCV.Id,
                            Name = templateSectionRefer.Name,
                            Order = templateSectionRefer.Order,
                            StyleJson = templateSectionRefer.StyleJson,
                            TypeSectionId = templateSectionRefer.TypeSectionId
                        };
                        await _sectionRespository.AddAsync(sectionRefer);
                        // End Insert Section insert Refer

                        //************************************************************************************************************************************
                        // Start Insert Section Additional
                        // convert request to object insert
                        // serilize object
                        // insert to database
                        var templateSectioninformation = await _sectionRespository.GetByIdAsync(request.information.SectionID);
                        var insertinformation = new SectionInformationInsert()
                        {
                            Description = request.information.Description
                        };
                        var insertinformationSer = JsonConvert.SerializeObject(insertinformation);
                        var sectioninformation = new Sections()
                        {
                            ContentJson = insertinformationSer,
                            cvId = newCV.Id,
                            Name = templateSectioninformation.Name,
                            Order = templateSectioninformation.Order,
                            StyleJson = templateSectioninformation.StyleJson,
                            TypeSectionId = templateSectioninformation.TypeSectionId
                        };

                        await _sectionRespository.AddAsync(sectioninformation);
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

        //Luu anh
        private async Task<string> SaveImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/CV/image_user", image.FileName);
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return image.FileName;
        }

        [HttpPost]
        public async Task<JsonResult> UploadImage(IFormFile file)
        {
            var path = await SaveImage(file);

            if (!string.IsNullOrEmpty(path))
            {
                return Json(new { success = true, imageUrl = path });
            }
            return Json(new { success = false });
        }

      
    }
}
