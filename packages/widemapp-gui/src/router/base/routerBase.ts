import Vue from 'vue'
import VueRouter, { RouteConfig } from 'vue-router'
import RestrictedRoutes from '../restrictedRoutes';
import UnrestrictedRoutes from '../unrestrictedRoutes';
import auth from './middlewares/authMiddleware';

var unrestrictFilter: Array<RouteConfig> = [];

for(var datas of UnrestrictedRoutes){
  // datas.meta = ["AllowAnnonymous"];
  // datas.meta.push(datas.meta)
  unrestrictFilter.push(datas);
}

const routes: Array<RouteConfig> = [...RestrictedRoutes, ...unrestrictFilter];

Vue.use(VueRouter)

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes: routes
})


function nextFactory(context: any, middleware: any, index: any) {
  const subsequentMiddleware = middleware[index];
  // If no subsequent Middleware exists,
  // the default `next()` callback is returned.
  if (!subsequentMiddleware) return context.next;

  return (...parameters: any) => {
    // Run the default Vue Router `next()` callback first.
    context.next(...parameters);
    // Then run the subsequent Middleware with a new
    // `nextMiddleware()` callback.
    const nextMiddleware = nextFactory(context, middleware, index + 1);
    subsequentMiddleware({ ...context, next: nextMiddleware });
  };
}

router.beforeEach((to, from, next) => {
  const middleware = Array.isArray(auth)
    ? auth
    : [auth];

  const context = {
    from,
    next,
    router,
    to,
  };

  const nextMiddleware = nextFactory(context, middleware, 1);

  return middleware[0]({ ...context, next: nextMiddleware });
});


export default router;
