export class TaskViewModel {
    id              : string = ''
    title           : string = ''
    description     : string = ''
    isImportant     : boolean = false
    isComplited     : boolean = false
    expirationTime  : Date = new Date()
    creationTime     : Date = new Date()
}