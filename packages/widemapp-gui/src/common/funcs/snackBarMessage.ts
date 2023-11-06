import HttpStatusCodeEnum from "../enums/httpStatusCodeEnum";
import { SnackBar, SnackBarColor } from "./snackBar";


class SnackBarMessage {
    private snackBar: SnackBar = new SnackBar();

    public async Success(message: string): Promise<void> {
        await this.snackBar.ShowSnackBar(message, SnackBarColor.Success);
    }

    public async Info(message: string): Promise<void> {
        await this.snackBar.ShowSnackBar(message, SnackBarColor.Info);
    }

    public async Error(message: string): Promise<void> {
        await this.snackBar.ShowSnackBar(message, SnackBarColor.Error);
    }

    public async ShowErrors(error: any): Promise<void> {
        const message = error.response.data;
        if (error.response.status === HttpStatusCodeEnum.BAD_REQUEST) {
            if (typeof message.errors === 'object') {
                await this.Error(Object.values<string>(message.errors)[0][0]);
            } else if (message.errors == undefined) {
                await this.Error(Object.values<string>(message)[0]);
            }
        } else if (error.response!.status === HttpStatusCodeEnum.FORBIDDEN) {
            await this.Error("You have no permission for this action!");
        } else if (error.response!.status === HttpStatusCodeEnum.INTERNAL_SERVER_ERROR) {
            await this.Error("Server could not respond please try again!");
        } else if (error.response!.status == HttpStatusCodeEnum.CONFLICT) {
            await this.Error(Object.values<string>(message)[0]);
        }
    }
}

export default new SnackBarMessage;
