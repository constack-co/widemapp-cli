interface IAppointmentTime {
    id: string,
    startsAt: string,
    endsAt: string,
    appointmentDate: IAppointmentDate
}

interface IAppointmentDate {
    id: string,
    forDate: string
}

interface IQrCode {
    id: string,
    appointmetId: string
}

interface ITestType {
    id: string,
    name: string
}

interface ITestResult {
    id: string,
    name: string
}

interface IUnavailableAppointmentTimes {
    currentDuplicate: number,
    startsAt: string
}

interface IFilterRequest{
    search?: string,
    pageSize?: number,
    page?: number
}

interface IFilterResponse<TInterfaceModel> {
    data: TInterfaceModel,
    totalPages: number
}



interface IRoleData{
    [key:string]:string
}

type HeadersType = {
    name: string;
    value: string;
}

export {IRoleData ,IAppointmentTime, ITestResult, ITestType, IQrCode, IUnavailableAppointmentTimes, IFilterRequest, IFilterResponse, HeadersType};