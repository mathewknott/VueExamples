import Vue from 'vue';
import axios from 'axios';

const client = axios.create({
    baseURL: 'https://localhost:44377/nineLetter',
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
    return this.execute('get', '/patternList');
  },
  validatePattern (patternInput) {
    return this.execute('get', '/?patternInput={patternInput}');
  }
};
