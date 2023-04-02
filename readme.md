# Task manager app
The Task Manager Web Application is a useful tool for anyone looking to manage their tasks more efficiently. With its easy-to-use features and intuitive user interface, users can stay on top of their work and complete tasks in a timely and organized manner.

<h2>🧐 About app</h2>
<p>Task manager will help you organize your schedule and not forget about important things. It allows you to save tasks until they are completed.</p>
<p>This application was made as a pet project to showcase skills: building a back-end api and user interface, deploying the application infrastructure on a third-party server</p>

<h2>Functionality</h2>
<ul>
  <li>User Registration and Login: Users can create a new account or log in to an existing account using their email address and password. Once logged in, users are redirected to the dashboard where they can view all of their tasks.</li>
  <li>Add New Task: Users can add new tasks to their account, including a name, description, expiration date, and whether the task is important or not. This feature    helps users keep track of their tasks and prioritize their work.</li>
  <li>Task Management: Once a user has added a task, they can view it in the dashboard and mark it as completed once it is done. The task will then disappear from the dashboard and be marked as completed.</li>
  <li>User Experience: The application has a clean and intuitive user interface that makes it easy for users to manage their tasks. The application's features are designed to be easy to use, and users can easily navigate through the different sections of the application.</li>
</ul>
<h2>🚀 Touch it!</h2>
<a href="https://taskmanager-plus.herokuapp.com/">I already deployed it to the web</a>

<h2>🛠️ Local running</h2>
<p>If you want to run it locally just follow the steps</p>
<ol>
  <li> Make sure you have the tools to compile .Net 6 projects </li>
  <li> Clone the repository </li>
  </ol>
  
```
git clone https://github.com/VitaliyMinaev/TaskManager.git
```

<ol start="3">
  <li>Go to directory: '...\Backend\TaskManager.Api'</li>
  <li>Run the app (it will run in <code>https://localhost:7142</code> url) </li>
  </ol>
  
``` cmd
dotnet run
```

<ol start="5">
  <li>Application will run with in memory database. It means that if you restart up, data will be destroyed. 
    To use your personal mssql server pass connection string as environment variable  with key <code>ConnectionString</code>.</li>
  </ol>
  
 <h2>💻 Frameworks and tools used in the project </h2> 
 <ul>
  <li> Asp .Net Core Web Api</li>
  <li>Angular</li>
  <li>MSSql</li>
  <li>Docker</li>
  <li>Swagger</li>
  <li>Google cloud</li>
  </ul>
  
<h2>👨‍💻 Technologies used in the project </h2> 
 <ul>
  <li>Authorization and authentication based on jwt tokens</li>
  <li>3 tier architecture</li>
  <li>Integration testing</li>
  <li>DI containers</li>
  </ul>
 
<h2>🧿 UI design pictures </h2>
<!-- Home page -->
<p align="center">
  <img src="https://user-images.githubusercontent.com/87979065/220935325-a0a860b7-530d-450b-84d6-557fd8cd7b64.png" width="1100">
 </p>
<!-- Task page -->
<p align="center">
  <img src="https://user-images.githubusercontent.com/87979065/220910644-5e964c6d-68b4-4b1e-87ab-05043d6ab181.png" width="1100">
 </p>
<p align="center">
  <img src="https://user-images.githubusercontent.com/87979065/220912989-94decac0-e758-4e6e-b2dd-cf427af50368.png" width="1100">
 </p>

 
