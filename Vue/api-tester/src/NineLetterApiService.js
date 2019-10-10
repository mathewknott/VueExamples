import Vue from 'vue';
import axios from 'axios';

const client = axios.create({
    baseURL: 'https://localhost:44377/api/nineLetter',
    json: true
});

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
  getList () {
      return this.execute('get', '/patterns');
  },
  validatePattern (patternInput) {
      return this.execute('get', '/validate?patternInput={patternInput}');
  }
}
