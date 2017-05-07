variable "region" {
  default = "eu-west-1"
}

module "experience" {
  source = "github.com/mtranter/cv-overkill-terraform?ref=v1.0//modules/tf-cv-overkill-aurelia-module"
  website_files = ["bundle.js", "bundle-cfg.js"]
  relative_source_path = "/../src/ui/dist/"
  region = "${var.region}"
  module_name = "experience"
}
