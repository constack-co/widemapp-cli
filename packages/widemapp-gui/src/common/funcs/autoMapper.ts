import { UnwrapRef } from "@vue/composition-api";

class AutoMapper {
    public async MapDataAsync<T>(origin:UnwrapRef<T|any>, destination:UnwrapRef<T|any>):Promise<void>{
        for (let key in origin) {
          if(origin.hasOwnProperty(key))
            destination[key] = origin[key];
        }
    }

    public async MapDataReverseAsync<T>(origin:UnwrapRef<T|any>, destination:UnwrapRef<T|any>):Promise<void>{
      for (let key in destination) {
        if(destination.hasOwnProperty(key))
          destination[key] = origin[key];
      }
    }

    public async HaveSameDataAsync<T>(origin:UnwrapRef<T|any>, destination:UnwrapRef<T|any>):Promise<boolean>{
      for (let key in origin) {
        if(origin.hasOwnProperty(key))
            if(destination[key] != origin[key]) return false;
        }
        return true;
    }

    public HaveSameData<T>(origin:UnwrapRef<T|any>, destination:UnwrapRef<T|any>):boolean{
      for (let key in origin) {
        if(origin.hasOwnProperty(key))
            if(destination[key] !== origin[key]) return  false;
        }
        return true;
    }

    public async HaveSameDataAsArrayAsync(a:[],b:[]):Promise<boolean>{
      if (a === b) return true;
      else if (a == null || b == null) return false;
      else if (a.length !== b.length) return false;

      for (let i = 0; i < a.length; ++i) {
        if (a[i] !== b[i]) return false;
      }
      return true;
    }
}

export default AutoMapper;
