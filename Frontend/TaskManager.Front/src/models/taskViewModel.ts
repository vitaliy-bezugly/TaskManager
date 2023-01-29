export class TaskViewModel {
    id: string
    title: string
    description: string
    expirationTime : Date
    createdTime : Date

    isImportant: boolean

    constructor() {
        this.id = ''
        this.title = ''
        this.description = ''
        this.expirationTime = new Date()
        this.createdTime = new Date()
        this.isImportant = false
    }
}