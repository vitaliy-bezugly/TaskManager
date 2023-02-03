export class CreateTaskRequest {
    title : string = ''
    description : string = ''
    isImportant : boolean = false
    expirationTime : Date = new Date()
}