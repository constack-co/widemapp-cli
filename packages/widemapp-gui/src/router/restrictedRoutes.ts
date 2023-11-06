import { RouteConfig } from 'vue-router'
import RouteNameEnum from '@/common/enums/routeNameEnum';

const importBase = (path: string): any => {
    return import("@/views/auth/" + path);
}

const RestrictedRoutes: Array<RouteConfig> = [
    {
        path: "/",
        component: () => importBase("home/AuthTemplate.vue"),
        children: [
            {
                path: "",
                name: RouteNameEnum.Dashboard,
                component: () => importBase("dashboard/Dashboard.vue"),
            },
            {
                path: "/entities",
                name: RouteNameEnum.Entities,
                component: () => importBase("entities/Entities.vue")
            },
            {
                path: "/plans",
                name: RouteNameEnum.Plans,
                component: () => importBase("plans/Plans.vue")
            },
            {
                path: "/templates",
                name: RouteNameEnum.Templates,
                component: () => importBase("templates/Templates.vue")
            },
            {
                path: "/templates/details",
                name: RouteNameEnum.TemplatesDetails,
                component: () => importBase("templates/templates-childs/OpenTemplate.vue")
            },
            {
                path: "/projects",
                name: RouteNameEnum.Projects,
                component: () => importBase("projects/Projects.vue")
            },
        ]
    }
]

export default RestrictedRoutes;
