import Vue from 'vue';
import axios from 'axios';

const client = axios.create({
  baseURL: 'https://localhost:44377/api/jokegenerator',
  json: true
})

export default {
  async execute (method, resource, data) {
    //const accessToken = await Vue.prototype.$auth.getAccessToken()
        return client({
            method,
            url: resource,
            data,
            headers: {
                //Authorization: `Bearer ${accessToken}`
            }
        }).then(req => {
            return req.data;
        });
  },
  getCategories () {
      return this.execute('get', '/getcategories');
  },
  getName () {
      return this.execute('get', '/getname');
  },
    getJoke(randomName, firstName, lastName, category) {
        return this.execute('get', `/getrandomjoke?randomName=${randomName}&firstName=${firstName}&lastName=${lastName}&category=${category}`);
  }
  // ,create (data) {
    // return this.execute('post', '/', data)
  // },
  // update (id, data) {
    // return this.execute('put', `/${id}`, data)
  // },
  // delete (id) {
    // return this.execute('delete', `/${id}`)
  // }
}
