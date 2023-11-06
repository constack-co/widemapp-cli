import store from "@/store/index";


enum SnackBarColor {
    Error = 0,
    Success = 1,
    Info = 2
}

class SnackBar {
    async ShowSnackBar(text: string, color: SnackBarColor):Promise<void> {
        let setColor:string;

        if(color == 0) setColor = 'red darken-4';
        else if (color == 1) setColor = 'green darken-2';
        else if (color == 2) setColor = 'orange darken-2';
        else setColor = 'green darken-2';

        await store.dispatch('setSnackBar', {
            show: true,
            text: text,
            color: setColor
        });
    }
}

export { SnackBarColor, SnackBar };
