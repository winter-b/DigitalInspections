<template>
    <div>
    <div class="work-create container mt-4">
      <h2 class="mb-3">Naujas darbas</h2>
      <form @submit.prevent="createWork">
        <div class="form-group">
          <label for="name">Pavadinimas</label>
          <input type="text" id="name" v-model="name" class="form-control" required>
        </div>
        <div class="form-group">
          <label for="category">Kategorija</label>
          <input type="text" id="category" v-model="category" class="form-control" required>
        </div>
        <div class="form-group">
          <label for="description">Aprašymas</label>
          <textarea id="description" v-model="description" class="form-control"></textarea>
        </div>
        <div class="form-group">
            <label for="nextInspection">Pabaigti iki</label>
            <input type="date" id="nextInspection" v-model="nextInspection">
        </div>
        <button type="submit" class="btn btn-success">Sukūrti</button>
      </form>
    </div>
    </div>
  </template>

  <script>
  console.log('WorkCreateView.vue');
  export default {
    data() {
      return {
        nextInspection: '',
        name: '',
        description: '',
        category: ''
      };
    },
    methods: {
      createWork() {
        const formData = new FormData();
        formData.append("machineId", this.$route.params.id);
        formData.append('nextInspection', this.nextInspection);
        formData.append('name', this.name);
        formData.append('description', this.description);
        formData.append('category', this.category);

        fetch('http://backend.ogversion.com/Machine/Work', {
          method: 'POST',
          headers: {
            accept: 'text/plain',
            'token': localStorage.getItem('token')
          },
          body: formData
        })
          .then(response => {
            if (response.ok) {
              // Work created successfully, handle the response as needed
              console.log('Work created successfully');
              // Redirect to the work list or navigate using Vue Router
              this.$router.push({ name: 'machine', params: { id: this.$route.params.id } });
            } else {
              // Handle error response
              console.error('Failed to create work');
            }
          })
          .catch(error => {
            // Handle fetch error
            console.error('An error occurred during work creation', error);
          });
      }
    }
  };
  </script>
  
  <style scoped>
  .container {
    max-width: 600px;
  }
  
  .form-group {
    margin-bottom: 1.5rem;
  }
  
  label {
    display: block;
    margin-bottom: 0.25rem;
  }
  
  .form-control {
    display: block;
    width: 100%;
    padding: 0.375rem 0.75rem;
    font-size: 1rem;
    line-height: 1.5;
    color: #495057;
    background-color: #fff;
    background-clip: padding-box;
    border: 1px solid #ced4da;
    border-radius: 0.25rem;
    transition: border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
  }
  
  .btn-primary {
    background-color: #007bff;
    border-color: #007bff;
  }
  </style>