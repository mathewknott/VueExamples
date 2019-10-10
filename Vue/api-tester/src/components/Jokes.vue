<template>
  <div class="container-fluid mt-4">
    <h1 class="h1">Jokes</h1>

    <p>Want to supply a name?  Yes <input type="checkbox" id="checkbox" v-model="jokeRandomChecked" @change="resetJoke"></p>
    <p>Want to supply a category?  Yes <input type="checkbox" id="checkbox" v-model="jokeCategoryChecked" @change="resetJoke"></p>

    <form @submit.prevent="check">

      <input type="text" v-show="jokeRandomChecked" v-model="firstName" class="form-control" placeholder="First Name" :required="jokeRandomChecked" />
      <br />
      <input type="text" v-show="jokeRandomChecked" v-model="lastName" class="form-control" placeholder="Last Name" :required="jokeRandomChecked" />
      <br />
      <select v-show="jokeCategoryChecked" v-model="categoryName" class="form-control" placeholder="Category Name" :required="jokeCategoryChecked">
        <option v-for="category in categories" v-bind:value="category">
          {{ category }}
        </option>
      </select>

      <p v-if="joke.length">Joke: {{ joke }}</p>

      <button class="btn btn-primary my-2" type="submit">Submit</button>

    </form>

    <h2 class="h2">Categories</h2>

    <b-alert :show="loading" variant="info">Loading...</b-alert>
    <b-row>
      <b-col>
        <table class="table table-striped">
          <thead>
            <tr>
              <th>Category</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="category in categories">
              <td>{{ category }}</td>
            </tr>
          </tbody>
        </table>
      </b-col>
    </b-row>
  </div>
</template>

<script>
  import api from '@/JokesApiService';

  export default {

    data() {
      return {
        loading: false,
        lastName: '',
        firstName: '',
        categoryName: '',
        categories: [],
        joke: '',
        jokeRandomChecked: false,
        jokeCategoryChecked: false,
        model: {}
      };
    },
    async created() {
      this.getCategoriesAll();

    },
    methods: {
      async check() {
          this.getJoke(!this.jokeRandomChecked,this.firstName,this.lastName,this.categoryName);
      },
      async getCategoriesAll() {
        this.loading = true

        try {
          this.categories = await api.getCategories()
        } finally {
          this.loading = false
        }
      },
      async getJoke(randomName, firstName, lastName, category) {
        this.loading = true

        try {
           let self = this;
          self.joke = await api.getJoke(randomName, firstName, lastName, category)
        } finally {
          this.loading = false
        }
      },
      async resetJoke() {
        this.joke = '';
        this.lastName = '';
        this.firstName = '';
        this.categoryName = '';
      }
    }
  }
</script>
