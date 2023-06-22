<template>
<div class="container">
  <div class="machine-update-view">
    <div class="machine-actions">
    <button @click="goBack()" class="btn btn-primary" style="float: left">Atgal</button>
    </div>
    <h2>Atnaujinti įrangą</h2>
    <form @submit.prevent="updateMachine">
      <div>
        <label for="name">Pavadinimas:</label>
        <input type="text" id="name" v-model="formData.name" required>
      </div>
      <div>
        <label for="category">Kategorija:</label>
        <input type="text" id="category" v-model="formData.category" required>
      </div>
      <div>
        <label for="description">Aprašymas:</label>
        <textarea id="description" v-model="formData.description" required></textarea>
      </div>
      <div>
        <button type="submit" class="btn btn-success">Update</button>
      </div>
    </form>
  </div>
</div>
</template>

<script>
export default {
  data() {
    return {
      formData: {
        name: '',
        category: '',
        description: ''
      }
    };
  },
  mounted() {
    this.fetchMachineData();
  },
  methods: {
    goBack() {
      this.$router.push({ name: 'home' });
    },
    async fetchMachineData() {
      try {
        const machineId = this.$route.params.id;
        const response = await fetch(`http://backend.ogversion.com/Machine?machineId=${machineId}`, {
          headers: {
            accept: 'text/plain',
            token: localStorage.getItem('token')
          }
        });
        const machineData = await response.json();
        this.formData = {
          name: machineData.name,
          category: machineData.category,
          description: machineData.description
        };
      } catch (error) {
        console.error('Error fetching machine data:', error);
      }
    },
    async updateMachine() {
      const machineId = this.$route.params.id;
      const form = new FormData();
            form.append("Name", this.formData.name);
            form.append("Description", this.formData.description);
            form.append("Category", this.formData.category);
            var response = await fetch(`http://backend/Machine?machineId=${machineId}`, {
                method: "PUT",
                headers: {
                    "accept": "text/plain",
                    "token": localStorage.getItem("token"),
                },
                body: form
            });
      this.$router.push({ name: 'home' });
    }
  }
};
</script>

<style scoped>
.machine-update-view {
    flex-direction: column;
    align-items: center;
    justify-content: center;
    padding: 20px;
}


form {
  margin-top: 20px;
}

label {
  display: block;
  margin-bottom: 5px;
}

input[type="text"],
textarea {
  width: 100%;
  padding: 8px;
  font-size: 16px;
  border: 1px solid #ccc;
  border-radius: 4px;
}
.machine-actions {
  display: inline-block;
}
</style>
