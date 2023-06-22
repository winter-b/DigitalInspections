<template>
  <div>
  <div class="container">
    <h1>Įrangos sąrašas</h1>
    <button @click="createMachine()" class="btn btn-primary">Kūrti naują</button>
    <table class="machine-table">
      <thead>
        <tr>
          <th class="hoverable" @click="sortMachines('name')">Pavadinimas</th>
          <th class="hoverable" @click="sortMachines('category')">Kategorija</th>
          <th class="hoverable" @click="sortMachines('lastUpdated')">Atnaujintas</th>
          <th style="display: flex; justify-content: flex-end; padding-right: 10%;">Veiksmas</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="machine in machines" :key="machine.id" class="machine" @click="viewMachine(machine.id)">
          <td class="name-column">{{ machine.name }}</td>
          <td>{{ machine.category }}</td>
          <td>{{ new Date(machine.lastUpdated).toLocaleString() }}</td>
          <td style="align-items: center; display: flex; justify-content: flex-end;">
            <button class="btn btn-primary" @click.stop="viewMachine(machine.id)">Peržiūrėti</button>
            <button v-if="roleClaim === 'Manager'" class="btn btn-warning" @click.stop="updateMachine(machine.id)">Atnaujinti</button>
            <button v-if="roleClaim === 'Manager'" class="btn btn-danger" @click.stop="deleteMachine(machine.id)">Ištrinti</button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</div>
</template>


<script>
import jwtdecode from 'jwt-decode';

export default {
  data() {
    return {
      machines: [],
      roleClaim: '',
      sortColumn: '', // Store the selected column name for sorting
      sortDirection: '', // Store the sort direction ('asc' or 'desc')
    };
  },
  mounted() {
    this.fetchMachines();
    this.fetchRoleClaim();
  },
  methods: {
    async fetchMachines() {
      try {
        const response = await fetch("http://backend.ogversion.com/Machines?limit=10&offset=0", {
          headers: {
            "accept": "text/plain",
            "token": localStorage.getItem("token")
          }
        });
        if (response.status === 401) {
          this.$router.push("/login");
          return;
        }
        this.machines = await response.json();
      } catch (error) {
        console.error('Error fetching machines:', error);
      }
    },
    sortMachines(parameter) {
      this.machines.sort((a, b) => {
        // Sort by the selected parameter
        if (a[parameter] < b[parameter]) {
          return this.sortDirection === 'asc' ? -1 : 1;
        }
        if (a[parameter] > b[parameter]) {
          return this.sortDirection === 'asc' ? 1 : -1;
        }
        return 0;
      });
    },
    fetchRoleClaim() {
      this.roleClaim = jwtdecode(localStorage.getItem("token")).role;
    },
    viewMachine(machineId) {
      this.$router.push({ path: `/machine/${machineId}` });
    },
    updateMachine(machineId) {
      this.$router.push(`/machine/edit/${machineId}`);
    },
    async deleteMachine(machineId) {
      await fetch(`http://backend.ogversion.com/Machine?machineId=${machineId}`, {
        method: "DELETE",
        headers: {
          "accept": "text/plain",
          "token": localStorage.getItem("token")
        }})
        .catch(error => {
          console.error("Error deleting machine:", error);
        });
      this.fetchMachines();
    },
    createMachine() {
      this.$router.push("/createMachine");
    },
  },
};
</script>

<style scoped>

table{
  width: 100%;
}
.machine-
table {
  width: 100%;
}

.machine-table th,
.machine-table td {
  padding: 10px;
}

.machine-table th {
  background-color: #f2f2f2;
}

.machine-table .name-column {
  cursor: pointer;
}

.machine-table .btn {
  margin-right: 5px;
}

.machine:hover {
  background: #ddd;
}

.hoverable:hover {
    cursor: pointer;
    background-color: rgb(135, 238, 242);
  }
</style>