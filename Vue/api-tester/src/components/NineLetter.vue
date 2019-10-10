<template>
  <div class="container-fluid mt-4">
    <h1 class="h1">Nineletter</h1>
    <b-alert :show="loading" variant="info">Loading...</b-alert>
    <b-row>
      <b-col>
        <table class="table table-striped">
          <thead>
          <tr>
            <th>Pattern</th>
            <th>Words</th>
            <th>Possible Words</th>
            <th>Longest Word</th>
          </tr>
          </thead>
          <tbody>
          <tr v-for="record in records" :key="record.id">
            <td>{{ record.pattern }}</td>
            <td>{{ record.words }}</td>
            <td>{{ record.possibleWords }}</td>
            <td>{{ record.longestWord }}</td>
          </tr>
          </tbody>
        </table>
      </b-col>
    </b-row>
  </div>
</template>

<script>
  import api from '@/NineletterApiService';

  export default {
    data() {
      return {
        loading: false,
        records: [],
        model: {}
      };
    },
    async created() {
      this.getAll()
    },
    methods: {
      async getAll() {
        this.loading = true

        try {
		 console.dir(api.getList());
          this.records = await api.getList()
		 
        } finally {
          this.loading = false
        }
      }
    }
  }
</script>
