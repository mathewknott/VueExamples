// Vue imports
import Vue from 'vue';
import Router from 'vue-router';

// 3rd party imports
import Auth from '@okta/okta-vue';

// our own imports
import Jokes from '@/components/Jokes';
import NineLetter from '@/components/NineLetter';

Vue.use(Auth, {
    issuer: 'https://dev-867692.oktapreview.com/oauth2/default',
    client_id: '0oafwzgamrftkVQ8e0h7',
    redirect_uri: 'http://localhost:8080/implicit/callback',
    scope: 'openid profile email'
});

Vue.use(Router);

let router = new Router({
    mode: 'history',
    routes: [
        {
            path: '/implicit/callback',
            component: Auth.handleCallback()
        },
        {
            path: '/nineletter',
            name: 'NineLetter',
            component: NineLetter,
            meta: {
                requiresAuth: false
            }
        },
        {
            path: '/jokes',
            name: 'Jokes',
            component: Jokes,
            meta: {
                requiresAuth: false
            }
        }
    ]
});

router.beforeEach(Vue.prototype.$auth.authRedirectGuard());

export default router;
