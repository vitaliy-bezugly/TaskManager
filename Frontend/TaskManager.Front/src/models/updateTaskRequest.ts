export class UpdateTaskRequest {
    title : string = ''
    description : string = ''
    isImportant : boolean = false
    isComplited : boolean = false
    expirationTime : Date = new Date()
}