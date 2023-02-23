<h1>Task manager app</h1>
<h2>🧐 About app</h2>
<p>Task manager will help you organize your schedule and not forget about important things. It allows you to save tasks until they are completed.</p>
<p>This application was made as a pet project to showcase skills: building a back-end api and user interface, deploying the application infrastructure on a third-party server</p>

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

 
