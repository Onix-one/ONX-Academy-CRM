import { Component, Inject} from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-courses',
  templateUrl: './courses.component.html'
})
export class CoursesComponent{
  public courses: ICourseDto[];
  public coursesAll: ICourseDto[];
  specializationId: number;

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<ICourseDto[]>(baseUrl + 'courses').subscribe(result => {
      this.courses = result;
      this.coursesAll = result;
    }, error => console.error(error));
  }

  onSelectSpecialization(specialization){
    if (specialization === "Choose...") {
      this.courses = this.coursesAll;
    }
    else {
      this.courses = this.coursesAll.filter(_ => _.specialization === specialization);
    }
  }
}

interface ICourseDto {
  id: number;
  title: string;
  description: string;
  necessaryPreKnowledge: string;
  cost: number;
  specializationId: number;
  specialization: string;
}
