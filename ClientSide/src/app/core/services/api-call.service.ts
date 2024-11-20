import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ApiCallService {

  rootUrl = 'https://localhost:7117/api/';

  constructor(private http: HttpClient) { }

  getList<T>(url: string): Observable<T[]> {
    return this.http.get<T[]>(this.rootUrl + url);
  }

  postList<T>(url: string, content: any, rootUrl = '/api/'): Observable<T[]> {
    return this.http.post<T[]>(this.rootUrl + url, content);
  }
  // Modify postInfo to set content type
  postInfo(url: string, content: any): Observable<string> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<any>(this.rootUrl + url, content, { headers });
  }

  getInfo<T>(url: string): Observable<T> {
    return this.http.get<T>(this.rootUrl + url);
  }

  getById<T>(url: string, id: number): Observable<T> {
    return this.http.get<T>(this.rootUrl + url + '/' + id);
  }

  UpdateDB<T>(url: string, content: any, rootUrl = '/api/'): Observable<T> {
    this.rootUrl = rootUrl;
    return this.http.post<T>(this.rootUrl + url, content);
  }

  GetFile(url: string, content: any): Observable<any> {
    return this.http.post(this.rootUrl + url, content, {
      responseType: "blob",
      headers: new HttpHeaders().append("Content-Type", "application/json")
    });
  }

  DeleteDB<T>(url: string, id: number): Observable<T> {
    const params = new HttpParams().set('id', id.toString());
    return this.http.delete<T>(this.rootUrl + url, { params });
  }

  UpdateFileDB<T>(url: string, content: any): Observable<T> {
    let fd: FormData = this.createFormData(content);
    return this.UpdateDB<T>(url, fd);
  }

  createFormData(object: any, form?: FormData, namespace?: string): FormData {
    const formData = form || new FormData();
    for (let property in object) {
      if (!object.hasOwnProperty(property) || object[property] === null || object[property] === undefined) {
        continue;
      }

      const formKey = namespace ? `${namespace}[${property}]` : property;

      if (object[property] instanceof Date) {
        formData.append(formKey, object[property].toISOString());
      } else if (typeof object[property] === 'object' && !(object[property] instanceof File)) {
        if (Array.isArray(object[property])) {
          for (let i = 0; i < object[property].length; i++) {
            const arrayItem = object[property][i];
            if (typeof arrayItem === 'object' && !Array.isArray(arrayItem) && !(arrayItem instanceof File)) {
              this.createFormData(arrayItem, formData, `${formKey}[${i}]`);
            } else {
              formData.append(`${formKey}[${i}]`, arrayItem);
            }
          }
        } else {
          this.createFormData(object[property], formData, formKey);
        }
      } else {
        formData.append(formKey, object[property]);
      }
    }
    return formData;
  }
}
