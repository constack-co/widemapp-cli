import {
    ref,
    Ref,
} from "@/importBase";


export const regexes = {
    birthDate: new RegExp("(([1-2][0-9])|([1-9])|(3[0-1]))/((1[0-2])|(0[1-9]))/[1-9][0-9]{3}$"),
    email: new RegExp(/^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@(([[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/),
    password: new RegExp(/^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$/)
}

class Rules {
    public simpleInputCounter = ref(100);
    public textAreaInputCounter = ref(500);

    public requiredInputRule: Ref<((value: any) => Boolean | String)[]> = ref([
        (value: string) => !!value || 'This field is required',
    ]);

    public simpleInputRule: Ref<((value: any) => Boolean | String)[]> = ref([
        (value: string) => !!value || 'This field is required',
        (value: string) => value ? value.length <= this.simpleInputCounter.value || "Maximum character reached!" : true,
    ]);

    public birthDayRule: Ref<((value: any) => Boolean | String)[]> = ref([
        (value: string) => !!value || 'This field is required',
        (value: string) => regexes.birthDate.test(value) || "Invalid Format!",
    ]);

    public passwordRule: Ref<((value: any) => Boolean | String)[]> = ref([
        (value: string) => !!value || 'This field is required',
        (value: string) => regexes.password.test(value) || "Weak Password!",
    ]);

    public emailRule: Ref<((value: any) => Boolean | String)[]> = ref([
        (value: string) => !!value || 'This field is required',
        (value: string) => regexes.email.test(value) || "Invalid email format!",
    ]);

    public unsignedNumber: Ref<((value: any) => Boolean | String)[]> = ref([
        (value: number) => (Math.sign(value) >= 0) || 'Number must be unsigned!',
    ]);

    public upperThanZero: Ref<((value: any) => Boolean | String)[]> = ref([
        (value: number) => (value > 0) || 'Number must be upper than zero!',
    ]);

    public deactivateUserRule: Ref<((value: any) => Boolean | String)[]> = ref([
        (value: string) => !!value || 'This field is required',
        (value: string) => value.toLowerCase() === "deactivate" || "Invalid Confirm code"
    ]);
}

export default Rules;
