# 📝 Student Enrollment Microservices 

This repository contains three different parts of folders. Each of the folder aka project is considered as a services with one single gateway that works as a middleman to connect both of the microservices. [Authentication service 🔑](https://github.com/jerichosiahaya/StudentEnrollmentMicroservices/tree/main/AuthenticationService) is a first layer of microservice that provides access token so user can make a request to another authorized service. [Enrollment service 📚](https://github.com/jerichosiahaya/StudentEnrollmentMicroservices/tree/main/EnrollmentService) provide CRUD services from three relational tables: `student`, `course`, and `enrollment`.

This diagram below will simply explain how these microservices works:

![flowchart](https://github.com/jerichosiahaya/StudentEnrollmentMicroservices/blob/main/assets/flowchart.jpg)

## How to install the project?
- Clone this repository to your local pc
- Open and run all the three projects at once
- I recommend you to use Kestrel as the local web server
- Enrollment service has `authorize` reservation which means you have to generate JWT Token from Authentication service
- I'll provide the SQL scheme and data later ✌

## API Documentation 🐒

### Authentication Service 🔑

| | HTTP Method        | Endpoint       |
|---| ------------- |:-------------:| 
|To generate token| POST      | `/api/auth/login` |
|To register a user| POST     | `/api/auth/register/admin`   |
|To register an admin| POST | `/api/auth/register/user`     |

### Enrollment Service 📝

<b>Student</b>

| | HTTP Method        | Endpoint       | Authorization Roles |
|---| ------------- |:-------------:| -------------------|
|Get all list of students| GET      | `/api/students` | Admin |
|Get student by id| GET     | `/api/students/id/{id}`   | All |
|Get student by username| GET | `/api/students/username/{id}`     | All |
|Insert student| POST | `/api/students`     | Admin |
|Update student| PUT | `/api/students/{id}`     | All |
|Delete student| DELETE | `/api/students/{id}`     | Admin |

<b>Course</b>

| | HTTP Method        | Endpoint       | Authorization Roles |
|---| ------------- |:-------------:| -------------------|
|Get all list of courses| GET      | `/api/courses` | All |
|Get course by id| GET     | `/api/courses/{id}`   | All |
|Insert course| POST | `/api/courses`     | Admin |
|Update course| PUT | `/api/courses/{id}`     | Admin |
|Delete course| DELETE | `/api/courses/{id}`     | Admin |

<b>Enrollment</b>

| | HTTP Method        | Endpoint       | Authorization Roles |
|---| ------------- |:-------------:| -------------------|
|Get all list of enrollments| GET      | `/api/enrollments` | Admin |
|Get enrollment by id| GET     | `/api/enrollments/{id}`   | All |
|Insert enrollment| POST | `/api/enrollments`     | All |
|Update enrollment| PUT | `/api/enrollments/{id}`     | Admin |
|Delete enrollment| DELETE | `/api/enrollments/{id}`     | All |

### API Rate Limiting

```
"RateLimitOptions": {
        "EnableRateLimiting": true,
        "Period": "60m",
        "PeriodTimespan": 300,
        "Limit": 100
      }
```
This API is limited to only make `100` request in period time of `60 minutes`, after the limit exceeded user must wait `5 minutes` before make another request. Also, the access token is only valid to an hour, after that user have to generate another access token since I don't provide refresh token function 🐵.


